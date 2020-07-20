(function ($) {
    angular.module('designer').requires.push('sfSelectors');

    angular.module('designer').controller('SimpleCtrl', [
        '$scope',
        'propertyService',
        function ($scope, propertyService) {
            $scope.feedback.showLoadingIndicator = true;
            $scope.uspSelector = {
                selectedItemsId: [],
                selectedItems: []
            };

            $scope.$watch(
                'uspSelector.selectedItems',
                function (newValue, oldValue) {
                    if (!!newValue && newValue !== oldValue) {
                        $scope.properties.SelectedUSP.PropertyValue = JSON.stringify(
                            newValue
                        );
                        var ids = newValue.map(function (item) {
                            return item.Id;
                        });
                        $scope.properties.SelectedUSPId.PropertyValue = JSON.stringify(ids);
                    }
                },
                true
            );

            propertyService
                .get()
                .then(
                    function (data) {
                        if (data) {
                            $scope.properties = propertyService.toAssociativeArray(
                                data.Items
                            );
                            if ($scope.properties.SelectedUSPId.PropertyValue) {
                                var selectedUSPIds = JSON.parse(
                                    $scope.properties.SelectedUSPId.PropertyValue
                                );
                                if (selectedUSPIds) {
                                    $scope.uspSelector.selectedItemsId = selectedUSPIds;
                                }
                            }
                            if ($scope.properties.SelectedUSP.PropertyValue) {
                                var selectedUSP = JSON.parse(
                                    $scope.properties.SelectedUSP.PropertyValue
                                );
                                if (selectedUSP) {
                                    $scope.uspSelector.selectedItems = selectedUSP;
                                }
                            }
                        }
                    },
                    function (data) {
                        $scope.feedback.showError = true;
                        if (data) $scope.feedback.errorMessage = data.Detail;
                    }
                )
                .finally(function () {
                    $scope.feedback.showLoadingIndicator = false;
                });
        }
    ]);
})(jQuery);
