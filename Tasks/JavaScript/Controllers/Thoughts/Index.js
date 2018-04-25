/// <reference path="../../framework/jquery-ui-1.12.1.js" />
/// <reference path="../../framework/jquery-1.10.2.intellisense.js" />
/// <reference path="../../framework/timedropper.js" />
/// <reference path="../../framework/monthpicker.js" />
/// <reference path="../../framework/moment.js" />
/// <reference path="../../framework/timeframe.js" />
/// <reference path="../../framework/layout.js" />

$(function () {
    thoughtsPubsub.init();
    thoughtsPubsub.applyBindings();
});

var thoughtsPubsub = {

    getThoughtId: function (e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },
    getSelectedThoughtId: function () { return $('#thoughtResultsTable tr.selected')[0].dataset.thoughtid; },

    urls: {
        DeleteThought: Core.ResolveUrl('Delete', 'Thoughts'),
        GetThoughtsTable: Core.ResolveUrl('GetThoughtsTable', 'Thoughts'),
        GetEditAside: Core.ResolveUrl('GetAsideEditSelectTab', 'Thoughts'),
        AddThought: Core.ResolveUrl('Create', 'Thoughts'),
        EditThought: Core.ResolveUrl('Update', 'Thoughts'),
        MoveThought: Core.ResolveUrl('Sort', 'Thoughts')
    },

    init: function () {

        thoughtsPubsub.initTable();
        // Show starting tab
        $('#aside-edit-container').hide();
        $('#add-container-timeframe').timeFrame();
    },

    initTable: function() {
        // Sortable
        $('#thoughtResultsTableBody').sortable({ tolerance: 'pointer' });
        $('#thoughtResultsTableBody').disableSelection();
        $('#thoughtResultsTableBody').sortable({
            axis: 'y',
            update: function (event, ui) {
                var line = ui.item.context;
                var thoughtId = line.dataset.thoughtid;
                var prevRow = line.previousElementSibling;
                var moveToSortId = 0;
                /*
                    If the line is dragged upwards then the previous line method returns +1 more than you'd expect
                    For this reason we need to work out the sortid differently depending on if the line is moving
                    upwards and downwards
                */
                if (prevRow != null) {
                    if (line.startRow < line.rowIndex) {
                        moveToSortId = prevRow.dataset.sortid;
                    } else {
                        moveToSortId = 1 + prevRow.dataset.sortid;
                    }
                }
                $.post(thoughtsPubsub.urls.MoveThought,
                    { thoughtId: thoughtId, moveToSortId: moveToSortId },
                    function (result) {
                        if (result === "True" || result == true) {
                            thoughtsPubsub.getTable();
                        }
                    }
                );
            },
            start: function (e, ui) {
                ui.item.context.startRow = ui.item.context.rowIndex;
            }
        });
    },

    initEditTimeFrame: function() {
        
    },

    addThought: function (e) {
        e.preventDefault();
        var viewModel = {
            Id: 0,
            Description: $('#add_description').val(),
            TimeFrameId: $('#addThoughtForm .timeframe-container .timeframe-output-type').val(),
            TimeFrameDate: moment($('#addThoughtForm .timeframe-container .timeframe-output-date').val()).format('DD/MM/YYYY'),
            TimeFrameTime: moment($('#addThoughtForm .timeframe-container .timeframe-output-time').val()).format('hh:mm')
        };
        $.post(thoughtsPubsub.urls.AddThought, viewModel,
            function(result) {
                if (result === "True" || result == true) {
                    thoughtsPubsub.getTable();
                    $(document).trigger('added');
                }
            });
    },

    applyBindings: function() {
        //Add
        $(document).on('click', '#btnAdd', this.addThought);
        $(document).on('submit', '#addThoughtForm', this.addThought);
        $(document).on('added', this.resetAddTab);
        //Edit
        $(document).on('click', '#btnEdit', this.editThought);
        $(document).on('submit', '#editThoughtForm', this.editThought);
        $(document).on('dblclick', '#thoughtResultsTableBody > tr', this.select);
        $(document).on('selected', this.getEditTab);
        //Delete
        $(document).on('click', '#thoughtResultsTableBody tr>td.btnDelete', this.deleteThought);
        //Table
        $(document).on('click', '#btnClose', this.deleteThought);
        $(document).on('mouseenter', '#thoughtResultsTableBody > tr', this.showRowOptions);
        $(document).on('mouseleave', '#thoughtResultsTableBody > tr', this.hideRowOptions);
        //Layout
        $(document).on('asideToggled', this.asideToggled);
        $(document).on('asideSwitched', this.asideSwitched);
    },

    editThought: function(e) {
        e.preventDefault();
        var viewModel = {
            Id:                  $('#edit_thoughtId').val(),
            Description:         $('#edit_description').val(),
            TimeFrameId:         $('#editThoughtForm .timeframe-container .timeframe-output-type').val(),
            TimeFrameDateString: $('#editThoughtForm .timeframe-container .timeframe-output-date').val(), 
            TimeFrameTimeString: $('#editThoughtForm .timeframe-container .timeframe-output-time').val()
        };
        $.post(thoughtsPubsub.urls.EditThought,
            viewModel,
            function(result) {
                if (result === "True" || result == true) {
                    layoutpubsub.closeAside();
                    thoughtsPubsub.getTable();
                    thoughtsPubsub.deselect();
                } else {
                    thoughtsPubsub.showError(result);
                }

            }
        );
    },

    deleteThought: function(e) {
        $.post(
            thoughtsPubsub.urls.DeleteThought,
            { thoughtId: thoughtsPubsub.getThoughtId(e) },
            function(result) {
                if (result === "True" || result == true) {
                    $(e.target).closest('tr').velocity('fadeOut',
                        {
                            duration: 300,
                            complete: function() {
                                thoughtsPubsub.getTable();
                            }
                        });
                }
            });
    },

    asideToggled: function () {
        if (!layoutpubsub.asideIsVisible) {
            thoughtsPubsub.deselect();
        }
    },

    asideSwitched: function() {
        var activeTab = layoutpubsub.getAsideActiveTab();
        if (activeTab !== 'aside-edit-container') {
            thoughtsPubsub.deselect();
        }
    },

    deselect: function () {

        $('#thoughtResultsTableBody .selected').removeClass('selected');

        var activeTab = layoutpubsub.getAsideActiveTab();
        var duration;

        if (layoutpubsub.asideIsVisible && activeTab !== 'aside-edit-container') {
            duration = 300;
        } else {
            duration = 0;
        }

        $('#aside-edit-select-container').velocity('fadeOut',
            {
                duration: duration,
                complete: function () {
                    $('#aside-edit-noselect-container').velocity('fadeIn', duration);
                }
            });
    },

    getTable: function() {
        $.post(thoughtsPubsub.urls.GetThoughtsTable,
            function(resultHtml) {
                $('#thoughtResultsTable').html(resultHtml);
                thoughtsPubsub.initTable();
            });
    },

    getEditTab: function () {
        var thoughtId = 0;
        thoughtId = thoughtsPubsub.getSelectedThoughtId;
        $.get(
            thoughtsPubsub.urls.GetEditAside,
            { thoughtId: thoughtId },
            function(resultHtml) {
                $('#aside-edit-select-container').html(resultHtml);

                var
                    typeId = $('#edit_timeFrameId').val(),
                    date = $('#edit_date').val(),
                    time = $('#edit_time').val();

                $('#edit-container-timeframe').timeFrame({
                    timeFrameId: typeId,
                    date: date,
                    time: time
                });
                layoutpubsub.showAsideTab('aside-edit-container');
            }
        );
    },

    hideRowOptions: function(e) {
        $(e.target)
            .closest('tr')
            .find('.options')
            .removeClass('visible');
    },

    hideAside: function() {
        
    },

    resetAddTab: function() {
        $('#add_description').val(''); //Description
        $('#addThoughtForm .timeframe-container .timeframe-output-type').val(''); //TimeframeId
        $('#addThoughtForm .timeframe-container .timeframe-output-date').val(''); //TimeFrameDate
        $('#addThoughtForm .timeframe-container .timeframe-output-time').val(''); //TimeFrameTime
    },

    select: function (e) {
        $('#thoughtResultsTableBody .selected').removeClass('selected');
        $(e.target)
            .closest('tr')
            .addClass('selected');

        var duration;

        if (layoutpubsub.asideIsVisible) {
            duration = 300;
        } else {
            duration = 0;
        }

        $('#aside-edit-noselect-container').velocity('fadeOut',
            {
                duration: duration,
                complete: function () {
                    $(document).trigger('selected');
                    $('#aside-edit-select-container').velocity('fadeIn', duration);
                    layoutpubsub.openAside();
                }
            });
    },

    showError: function(error) {
        alert(error);
    },

    showRowOptions: function(e) {
        $(e.target)
            .closest('tr')
            .find('.options')
            .addClass('visible');
    },

    validateAddViewModel: function() {
        
    },

    validateEditViewModel: function() {
        
    }
};