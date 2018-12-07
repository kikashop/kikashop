﻿(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getslides = getslides;

        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteSlide = deleteSlide;

        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var count = 0;
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
                count = count + 1;
            });
            $ngBootbox.confirm('Xóa ' + count + ' mục đã chọn?').then(function () {
               
                var config = {
                    params: {
                        checkedSlides: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/slide/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                })
            });
           
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.slides, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("slides", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteSlide(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/slide/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }
        function search() {
            getslides();
        }

        function getslides(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/slide/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào!');
                }
                else {
                    notificationService.displaySuccess('Đã tìm được ' + result.data.TotalCount + ' bản ghi');
                }
                $scope.slides = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                console.log(' Load slide  failed');
            });
        }
     

        $scope.getslides();
    }
})(angular.module('kikashop.slides'));