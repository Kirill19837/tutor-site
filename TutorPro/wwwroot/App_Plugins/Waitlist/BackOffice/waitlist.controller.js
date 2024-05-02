angular.module("umbraco").controller("waitlist", function ($scope, $sce, $http) {
    $scope.selectedItems = [];
    $scope.menuPanelOpen = false;
    $scope.sortField = null;
    $scope.reverse = false;
    var entryDetails;
    var sortedDetails;

    $scope.currentPage = 1;
    $scope.pageSize = 20;

    $scope.getData = function () {
        $http.get("/api/WaitList")
            .then(function (response) {
                entryDetails = response.data;
                sortedDetails = response.data;
                $scope.updatePagination();
                console.log(entryDetails)
            })
            .catch(function (error) {
                console.error("Error fetching waitlist data:", error);
            });

        $scope.showDeletedData = false;
    };

    $scope.getDeletedData = function () {
        $http.get("/api/WaitList/deleted")
            .then(function (response) {
                entryDetails = response.data;
                sortedDetails = response.data;
                $scope.updatePagination();
            })
            .catch(function (error) {
                console.error("Error fetching waitlist data:", error);
            });
        $scope.showDeletedData = true;
    };

    $scope.removeSelectedItems = function () {
        var ids = $scope.selectedItems.map(item => item.id);
        console.log(ids);
        $http.post("/api/WaitList/range_remove", ids)
            .then(function (response) {

                entryDetails = entryDetails.filter(function (item) {
                    return !ids.includes(item.id);
                });
                
               console.log($scope.paginatedSortedDetails)
                $scope.selectedItems.map(row => {
                    row.parentNode.parentNode.parentNode.removeChild(row);
                })                  
                
                $scope.clearSelect();
            })
            .catch(function (error) {
                console.error("Error removing waitlist users:", error);
            });
    };

    $scope.gotoPage = function (page) {
        if (page >= 1 && page <= $scope.pageCount()) {
            $scope.currentPage = page;
            $scope.updatePagination();
        }
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage--;
            $scope.updatePagination();
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
            $scope.updatePagination();
        }
    };

    $scope.toggleMenuPanel = function () {
        $scope.menuPanelOpen = !$scope.menuPanelOpen;
    };

    $scope.clearSelect = function () {
        angular.forEach($scope.selectedItems, function (item) {
            item.selected = false;
        });

        $scope.selectedItems = [];
    };

    $scope.toggleSelect = function (row) {
        var index = $scope.selectedItems.indexOf(row);
        if (index === -1) {
            $scope.selectedItems.push(row);
            row.selected = true;
        } else {
            $scope.selectedItems.splice(index, 1);
            row.selected = false;
        }
    };

    $scope.updatePagination = function () {
        var startIndex = ($scope.currentPage - 1) * $scope.pageSize;
        var endIndex = Math.min(startIndex + $scope.pageSize, entryDetails.length);
        
        $scope.paginatedSortedDetails = sortedDetails.slice(startIndex, endIndex);
    };

    $scope.pageCount = function () {
        if (!$scope.sortedDetails) {
            return 0;
        }
        return Math.ceil(sortedDetails.length / $scope.pageSize);
    };

    $scope.sortBy = function (field) {
        if ($scope.sortField === field) {
            $scope.reverse = !$scope.reverse;
        } else {
            $scope.sortField = field;
            $scope.reverse = false;
        }

        entryDetails.sort(function (a, b) {
            if (field === "createDate" || field === "deletedDate") {
                if ($scope.reverse) {
                    return (new Date(a[field]) < new Date(b[field])) ? 1 : -1;
                } else {
                    return (new Date(a[field]) > new Date(b[field])) ? 1 : -1;
                }
            } else {
                return (a[field] > b[field]) ? 1 : -1;
            }
        });

        $scope.updatePagination();
    };

    $scope.isSortedBy = function (columnName, direction) {
        return $scope.sortField === columnName && ($scope.reverse ? direction === 'desc' : direction === 'asc');
    };

    $scope.filterChanged = function (searchText) {
        if (!searchText) {
            sortedDetails = entryDetails;
            $scope.updatePagination();
            return;
        }

        sortedDetails = entryDetails.filter(function (item) {
            return (
                (item.phoneNumber && item.phoneNumber.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.email && item.email.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.name && item.name.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.message && item.message.toLowerCase().includes(searchText.toLowerCase()))
            );
        });

        $scope.gotoPage(1);
    };
});
