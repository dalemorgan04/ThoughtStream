$(function () {
    $('table tbody').sortable();
    pubsub.applyBindings();
});

var pubsub = {

    urls: {
        GetAddTaskPopup: Core.ResolveUrl('GetAddTask', 'Inbox')
    },

    applyBindings: function () {
        $(document).on('click', '.input[type = "submit"]', this.SaveTask);
        $(document).on('click', '#AddTask', this.showAddTask);
        $(document).on('click', '#btnClose', this.closePopup);
    },

    showAddTask: function (e) {
        $.post(pubsub.urls.GetAddTaskPopup, pubsub.showPopup);
    },

    saveTask: function () {
        var viewModel = $(e.target).closest('form').serialize();
        $.post("Inbox.aspx/Create",
            viewModel,
            function (result) {

            });
    },

    showPopup: function (popupHtml) {
        $('#AddTaskPopup').html(popupHtml);
        $('#AddTaskModal').modal();
    },

    closePopup: function () {
        $('#AddTaskModal').modal();
    },

    validateForm: function (e) {
        return true;
    }
};