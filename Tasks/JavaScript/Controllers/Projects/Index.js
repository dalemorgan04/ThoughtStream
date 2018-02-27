$(function() {
    projectsPubsub.init();
});

var projectsPubsub = {
    urls: {
        getAddProject: urlsPubsub.getAddProject,
        getEditProject: urlsPubsub.getEditProject,
        getProjectsTable: urlsPubsub.getProjectsTable,
        getAddTask: urlsPubsub.getAddTask,
        appendTask: urlsPubsub.appendTask,
        saveProject: urlsPubsub.saveProject
    },

    applyBindings: function() {
        $(document).on('click', '#aside-tabs-add', this.asideShowAdd);
        $(document).on('click', '#aside-tabs-edit', this.asideShowEdit);
        $(document).on('click', '#aside-tabs-tasks', this.asideShowTasks);
    },

    init: function () {

        projectsPubsub.applyBindings();
        projectsPubsub.connectTables();
    },

    asideShowAdd: function () {
        
        $('#aside-content > div')
            .velocity('finish')
            .hide();

        $('#aside-add-container').velocity('fadeIn', { display: 'block' });
    },

    asideShowEdit: function() {
        //Hide
        projectsPubsub.hideAllTabs();

        //Get new html
        $.ajax({
            url: projectsPubsub.urls.getEditProject,
            type: 'POST',
            data: { taskId: selectedId },
            success: function (resultHtml) {

                //Hide visible tab
                $('#aside-content > div:visible')
                    .velocity('stop', true)
                    .velocity('fadeOut',
                        { display: 'none' },
                        {
                            complete: function() {

                                $('#aside-edit-container')
                                    .velocity('stop', true)
                                    .prop('display', 'none')
                                    .html(resultHtml);
                            }
                        });

            }
        });

        //Reapply bindings
        $(document).on('click', '#aside-tabs-tasks', this.editProject);
    },

    asideShowTasks: function() {
        //Hide
        projectsPubsub.hideAllTabs();
        
        //Get new html
        $.ajax({
            url: projectsPubsub.urls.getAddTask,
            type: 'POST',

            beforeSend: function () {
                $('#aside-content > div:visible')
                    .velocity('finish')
                    .velocity('fadeOut', { display: 'none' });
            },

            success: function (resultHtml) {

                $('#aside-content > div')
                    .velocity('finish');

                $('#aside-tasks-container')
                    .prop('display', 'none')
                    .html(resultHtml)
                    .velocity('fadeIn', { display: 'block' });
                
                //Connect tasks to project tables
                $('.connectedSortable').sortable({
                    items: '>*:not(.emptyTablePlaceholder)',
                    connectWith:'.connectedSortable',
                    helper: 'clone',
                    zIndex: 10000,
                    placeholder: 'placeholder'
                });
            }
        });

        
        
    },

    asideResetAdd: function() {
        
    },

    appendTaskToProject: function(projectId, taskId) {

        $.ajax({
            url: projectsPubsub.urls.appendTask,
            data: {projectId: projectId, taskId: taskId},
            type: 'POST',
            success: function() {
                projectsPubsub.getProjectsTable();
            }
        });


    },

    connectTables: function() {
        //Connect tasks to project tables
        $('.connectedSortable').sortable({
            items: '>*:not(.emptyTablePlaceholder)',
            receive: function (event, ui) {
                projectsPubsub.appendTaskToProject(event.target.dataset.projectid, ui.item["0"].dataset.taskid);
            },
            connectWith: '.connectedSortable',
            helper: 'clone',
            zIndex: 10000,
            placeholder: 'placeholder'
        });
    },

    editProject: function() {
        
    },

    getProjectsTable: function() {
        $.ajax({
            url: projectsPubsub.urls.getProjectsTable,
            type: 'GET',
            success: function (resultHtml) {
                $('#projectResultsTable').html(resultHtml);
                projectsPubsub.connectTables();
            }
        });
    },

    hideAllTabs: function() {
        if ($('#aside-add-container').is(':visible')) {
            $('#aside-add-container')
                .velocity('stop', true)
                .velocity('fadeOut', { display: 'none' });
        }

        if ($('#aside-edit-container').is(':visible')) {
            $('#aside-edit-container')
                .velocity('stop', true)
                .velocity('fadeOut', { display: 'none' });
        }

        if ($('#aside-tasks-container').is(':visible')) {
            $('#aside-tasks-container')
                .velocity('stop', true)
                .velocity('fadeOut', { display: 'none' });
        }
    },

    saveProject: function() {

    },

    selectProject: function() {

    }
}