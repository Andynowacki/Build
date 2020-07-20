(function ($) {
    angular.module('designer').requires.push('sfSelectors');

    angular.module('designer').controller('SimpleCtrl', [
        '$scope',
        'propertyService',
        function ($scope, propertyService) {
            $scope.feedback.showLoadingIndicator = true;

            $scope.$watch(
                'selectedItem',
                function (newVal, oldVal) {
                    if (!!newVal && newVal !== oldVal) {
                        $scope.properties.SelectedTestimonial.PropertyValue = JSON.stringify(
                            newVal
                        );
                    }
                },
                true
            );

            $scope.$watch(
                'selectedItemId',
                function (newVal, oldVal) {
                    if (!!newVal && newVal !== oldVal) {
                        $scope.properties.SelectedTestimonialId.PropertyValue = newVal;
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

                            if ($scope.properties.SelectedTestimonialId.PropertyValue) {
                                if ($scope.properties.SelectedTestimonialId.PropertyValue) {
                                    $scope.selectedItemId =
                                        $scope.properties.SelectedTestimonialId.PropertyValue;
                                }
                            }

                            if ($scope.properties.SelectedTestimonial.PropertyValue) {
                                var SelectedTestimonial = JSON.parse(
                                    $scope.properties.SelectedTestimonial.PropertyValue
                                );
                                if (SelectedTestimonial) {
                                    $scope.selectedItem = SelectedTestimonial;
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
