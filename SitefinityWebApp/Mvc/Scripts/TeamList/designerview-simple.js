(function($) {
  angular.module('designer').requires.push('sfSelectors');

  angular.module('designer').controller('SimpleCtrl', [
    '$scope',
    'propertyService',
    function($scope, propertyService) {
      $scope.feedback.showLoadingIndicator = true;
      $scope.itemSelector = {
        selectedItemsId: [],
        selectedItems: []
      };

      $scope.$watch(
        'itemSelector.selectedItems',
        function(newValue, oldValue) {
          if (!!newValue && newValue !== oldValue) {
            $scope.properties.SelectedItem.PropertyValue = JSON.stringify(
              newValue
            );
            var ids = newValue.map(function(item) {
              return item.Id;
            });
            $scope.properties.SelectedItemId.PropertyValue = JSON.stringify(ids);
          }
        },
        true
      );

      propertyService
        .get()
        .then(
          function(data) {
            if (data) {
              $scope.properties = propertyService.toAssociativeArray(
                data.Items
              );
              if ($scope.properties.SelectedItemId.PropertyValue) {
                var selectedItemIds = JSON.parse(
                  $scope.properties.SelectedItemId.PropertyValue
                );
                if (selectedItemIds) {
                  $scope.itemSelector.selectedItemsId = selectedItemIds;
                }
              }
              if ($scope.properties.SelectedItem.PropertyValue) {
                var selectedItem = JSON.parse(
                  $scope.properties.SelectedItem.PropertyValue
                );
                if (selectedItem) {
                  $scope.itemSelector.selectedItems = selectedItem;
                }
              }
            }
          },
          function(data) {
            $scope.feedback.showError = true;
            if (data) $scope.feedback.errorMessage = data.Detail;
          }
        )
        .finally(function() {
          $scope.feedback.showLoadingIndicator = false;
        });
    }
  ]);
})(jQuery);
