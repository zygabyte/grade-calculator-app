let sessionId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Session/ReadSessions",
        null,
        true,
        readSessionsResponse, true);
    
}

function readSessionsResponse(data){
    resetDataTable($('#sessionTable'));

    if (data.status) {
        data.data.forEach((session, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + session.name + '</td>';
            row += '<td>' + session.code + '</td>';
            row += '<td><button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editSessionClick(\'' + session.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteSessionModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSessionClick(\'' + session.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#sessionTable tbody').append(row);
        });

        initializeDataTable($('#sessionTable'));
    }
}

function showCreateSessionButton() {
    $('#createSessionBtn').show();
    $('#editSessionBtn').hide();
    $('#createSessionTitle').show();
    $('#editSessionTitle').hide();

    resetField();
}

function createSession(){
    const name = $('#name').val();
    const code = $('#code').val();
    
    const session = {
        Name: name,
        Code: code
    };
    
    api('POST', '/Session/CreateSession', 
        {session: session}, true, createSessionResponse, true);
}

function createSessionResponse(data){
    if (data.status) onSuccessModalHide();
}

function editSessionClick(sessionId){
    api('GET', '/Session/ReadSession',
        {sessionId: sessionId}, true, editSessionClickResponse, true);
}

function editSessionClickResponse(data){
    if (data.status){
        $('#newSessionModal').modal('show');

        $('#createSessionBtn').hide();
        $('#editSessionBtn').show();
        $('#createSessionTitle').hide();
        $('#editSessionTitle').show();
        
        const session = data.data;

        $('#id').val(session.id);
        $('#name').val(session.name);
        $('#code').val(session.code);
    }
}

function editSession(){
    const sessionId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();
    
    const session = {
        Name: name,
        Code: code
    };
    
    api('POST', '/Session/UpdateSession',
        {sessionId: sessionId, session: session}, true, editSessionResponse, true);
}

function editSessionResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteSessionClick(sessionId) {
    window.sessionId = sessionId;
}

function deleteSession() {
    api('POST', '/Session/DeleteSession',
        {sessionId: window.sessionId}, true, deleteSessionResponse, true);
}

function deleteSessionResponse(data) {
    if (data.status) onSuccessModalHide();
}




//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newSessionModal').modal('hide');
    $('#deleteSessionModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
}