var employeePageController = (function(){
    function onDeptChange() {
        $('#employeeSearchForm').trigger('submit');
    }

    return {
        onDeptChange : onDeptChange
    }
})();