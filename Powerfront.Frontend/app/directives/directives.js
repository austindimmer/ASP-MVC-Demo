'use strict';

var directives = angular.module('impact.directives', []);

directives.directive('focus',
    function() {
  return {
    link: function(scope, element, attrs) {
      element[0].focus();
    }
  };
});
