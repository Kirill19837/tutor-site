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
        let endpoint = "range_remove";
        if ($scope.showDeletedData) {
            endpoint = "hard_range_remove"
        }
        var ids = $scope.selectedItems.map(item => item.id);
        $http.post(`/api/WaitList/${endpoint}`, ids)
            .then(function (response) {
                $scope.refresh(ids);
            })
            .catch(function (error) {
                console.error("Error removing waitlist users:", error);
            });
    };  

    $scope.restoreSelectedItems = function () {
        var ids = $scope.selectedItems.map(item => item.id);
        $http.post("/api/WaitList/range_restore", ids)
            .then(function (response) {
                $scope.refresh(ids);
            })
            .catch(function (error) {
                console.error("Error restoring waitlist users:", error);
            });
    };

    $scope.export = function () {
        $http.get("/api/WaitList/export", { responseType: 'arraybuffer' })
            .then(function (response) {
                var blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
                var url = window.URL.createObjectURL(blob);
                var a = document.createElement('a');
                a.href = url;
                a.download = 'waitlist.xlsx'; // file name
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
            })
            .catch(function (error) {
                console.error("Error exporting waitlist data:", error);
            });
    };

    $scope.refresh = function (ids)  {
        $scope.paginatedSortedDetails = $scope.paginatedSortedDetails.filter(function (item) {
            return !ids.includes(item.id);
        });
        entryDetails = entryDetails.filter(function (item) {
            return !ids.includes(item.id);
        });
        sortedDetails = sortedDetails.filter(function (item) {
            return !ids.includes(item.id);
        });
        $scope.updatePagination();
        $scope.clearSelect();
    }

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
        var endIndex = Math.min(startIndex + $scope.pageSize, sortedDetails.length);
        $scope.paginatedSortedDetails = sortedDetails.slice(startIndex, endIndex);
    };

    $scope.pageCount = function () {
        if (!sortedDetails) {
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
                if ($scope.reverse) {
                    return (a[field] < b[field]) ? 1 : -1;
                } else {
                    return (a[field] > b[field]) ? 1 : -1;
                }
                
            }
        });

        sortedDetails = entryDetails.slice(); // Clone the array
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

        var filteredDetails = entryDetails.filter(function (item) {
            return (
                (item.phoneNumber && item.phoneNumber.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.email && item.email.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.name && item.name.toLowerCase().includes(searchText.toLowerCase())) ||
                (item.message && item.message.toLowerCase().includes(searchText.toLowerCase()))
            );
        });

        sortedDetails = filteredDetails.slice(); // Clone the array
        $scope.updatePagination();
        $scope.gotoPage(1); // Reset to the first page after filtering
    };
});
