$(function () {
    tasksPubsub.init();
    tasksPubsub.applyBindings();
});

var tasksPubsub = {
    getThougtId: function(e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },

    urls: {
        GetAddTaskPopup: Core.ResolveUrl('GetAddTask', 'Tasks'),
        GetTasksTable: Core.ResolveUrl('GetTasksTable', 'Tasks'),
        SaveTask: Core.ResolveUrl('Create', 'Tasks')
    },

    init: function() {

    },

    applyBindings: function() {
        $(document).on('click', '#btnShowAddTask', this.ShowAddTaskPopup);
        $(document).on('click', '#btnAddTask', this.saveTask);
    },

    getTasksTable: function() {
        $.post(tasksPubsub.urls.GetTasksTable,
            function(resultHtml) {
                $('#taskResultsTable').html(resultHtml);
                tasksPubsub.init();
            });
    },

    saveTask: function() {
        e.preventDefault();
        var viewModel = $('#addTaskForm').serialize();
        $.post(tasksPubsub.urls.SaveThought,
            viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    tasksPubsub.getThoughtsTable();
                }
            });
    },

    ShowAddTaskPopup: function(e) {
        $.post(tasksPubsub.urls.GetAddTaskPopup,
            function (popupHtml) {
                $('#addTaskContainer').html(popupHtml);
                $('#addTaskModal').modal();
            }
        );
    },

    hideAddTask: function() {
        $('#addTaskModal').modal();
    }
}