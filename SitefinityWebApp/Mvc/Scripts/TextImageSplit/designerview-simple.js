(function($) {
  var simpleViewModule = angular.module('simpleViewModule', ['designer', 'ngSanitize']);
  angular.module('designer').requires.push('simpleViewModule');
  angular.module('designer').requires.push('sfFields');
  angular.module('designer').requires.push('sfSelectors');

  simpleViewModule.controller('SimpleCtrl', [
    '$scope',
    'propertyService',
    'sfLinkService',
    function($scope, propertyService, linkService) {
      $scope.proxy = {};
      $scope.feedback.showLoadingIndicator = true;

      propertyService
        .get()
        .then(
          function(data) {
            if (data) {
              $scope.properties = propertyService.toAssociativeArray(data.Items);

              $scope.currentLink = jQuery($scope.properties.Link.PropertyValue);
            }
          },

          function(data) {
            $scope.feedback.showError = true;

            if (data) $scope.feedback.errorMessage = data.Detail;
          }
        )

        .then(function() {
          $scope.feedback.savingHandlers.push(function() {
            var htmlLinkObj = linkService.getHtmlLink($scope.proxy.selectedItem);

            var found = htmlLinkObj[0].href.match(
              /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/
            );

            if (found) {
              $scope.properties.Link.PropertyValue = htmlLinkObj[0].outerHTML;
            } else {
              $scope.properties.Link.PropertyValue = '';
            }
          });
        })

        .finally(function() {
          $scope.feedback.showLoadingIndicator = false;
        });
    }
  ]);
})(jQuery);
