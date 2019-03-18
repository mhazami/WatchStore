/// <reference path="jquery-1.10.2.min.js" />


$(function () {

});




function ShowDefualtMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div>').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);


}

function ShowSuccessMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div style="box-shadow: 1px 1px 20px 7px rgba(80, 216, 47, 0.5);">').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);

}


function ShowErrorMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div style="box-shadow: 1px 1px 20px 7px rgba(228, 36, 36, 0.5);">').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);

}

function ShowWarningMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div style="box-shadow: 1px 1px 20px 7px rgba(245, 204, 4, 0.77);">').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);

}


function ShowInfoMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div style="box-shadow: 1px 1px 20px 7px rgba(4, 212, 245, 0.7);">').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);

}

function ShowPrimaryMessage(header, message) {
    var main = $('<div style="display:block;">').addClass('modal fade in').attr('id', 'ShowMessage').attr('role', 'dialog');
    var modal = $('<div>').addClass('modal-dialog');
    var content = $('<div style="box-shadow: 1px 1px 20px 7px rgba(39, 52, 210);">').addClass('modal-content');


    var head = $('<div>').addClass('modal-header');
    $('<button>')
        .addClass('close')
        .attr('data-dismiss', 'modal')
        .attr('type', 'button').append($('<span>').addClass('glyphicon glyphicon-remove').attr('onclick', 'CloseMessage();').attr('data-close', 'close'))
        .appendTo(head);
    $('<h4 style="text-align: center;">')
        .addClass('modal-title')
        .text(header)
        .appendTo(head);

    var body = $('<div>').addClass('modal-body');
    $('<p>').text(message).appendTo(body);

    var footer = $('<div>').addClass('modal-footer');
    $('<button>')
        .addClass('btn btn-default')
        .attr('data-close', 'close')
        .attr('onclick', 'CloseMessage();')
        .attr('type', 'button')
        .text('close')
        .appendTo(footer);

    content.append(head).append(body).append(footer);

    modal.append(content);

    main.append(modal);

    $('body').prepend(main);

}


function CloseMessage() {
    $("#ShowMessage").remove();
}

