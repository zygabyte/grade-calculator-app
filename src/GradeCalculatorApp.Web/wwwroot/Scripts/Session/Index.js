$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Session/ReadSessions",
        null,
        true,
        readSessionsResponse, true);
    
    api("GET",
        "/Semester/ReadSemesters",
        null,
        true,
        readSemestersResponse, true);
}

function readSessionsResponse(data) {
    console.log('sesion data');
    console.log(data);
    resetDataTable($('#sessionTable'));
    
    if (data.status) {
        data.data.forEach((session, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + session.name + '</td>';
            row += '<td>' + session.semester + '</td>';
            row += '<td>' + new Date(session.semesterStartDate).toDateString() + '</td>';
            row += '<td>' + new Date(session.semesterEndDate).toDateString() + '</td>';
            row += '<td> active </td>';
            row += '<td><button type="button" class="btn btn-primary btn-xs" onclick="mapCourses(\'' + session.id + '\')">Courses</button> | <button type="button" class="btn btn-success btn-xs" onclick="editSessionClick(\'' + session.id + '\')">Edit</button> | <a href="#deleteSessionModal" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSessionClick(\'' + session.id + '\')">Delete</a></td>';
            row += '</tr>';

            $('#sessionTable tbody').append(row);
        });

        initializeDataTable($('#sessionTable'));
    }
}

function readSemestersResponse(data){
    if (data.status) {
        data.data.forEach((semester) => {
            $('#semester').append(`<option value=${semester.id}>${semester.name}</option>`);
        });
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
    const session = $('#session').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();

    const sessionObject = {
        Name: session,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate
    };

    api('POST', '/Session/CreateSession',
        {session: sessionObject}, true, createSessionResponse, true);
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
        $('#session').val(session.name);
        $('#semester').val(session.semesterId);
        $('#startDate').val(session.semesterStartDate);
        $('#endDate').val(session.semesterEndDate);
    }
}

function editSession(){
    const sessionId = $('#id').val();

    const session = $('#session').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();

    const sessionObject = {
        Name: session,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate
    };

    api('POST', '/Session/UpdateSession',
        {sessionId: sessionId, session: sessionObject}, true, editSessionResponse, true);
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
    $('#session').val('');
    $('#semester').val(0);
    $('#startDate').val('');
    $('#endDate').val('');
}