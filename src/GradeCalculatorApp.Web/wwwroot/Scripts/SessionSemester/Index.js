$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/SessionSemester/ReadSessionSemesters",
        null,
        true,
        readSessionSemestersResponse, true);
    
    const semesterDropDownLength = $('#semester > option').length;
    const sessionDropDownLength = $('#session > option').length;
    
    if(semesterDropDownLength === 1)
        api("GET",
            "/Semester/ReadSemesters",
            null,
            true,
            readSemestersResponse, true);

    if(sessionDropDownLength === 1)
    api("GET",
        "/Session/ReadSessions",
        null,
        true,
        readSessionsResponse, true);
}

function readSessionSemestersResponse(data) {
    resetDataTable($('#sessionSemesterTable'));
    
    if (data.status) {
        data.data.forEach((sessionSemester, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + sessionSemester.session + '</td>';
            row += '<td>' + sessionSemester.semester + '</td>';
            row += '<td>' + new Date(sessionSemester.semesterStartDate).toDateString() + '</td>';
            row += '<td>' + new Date(sessionSemester.semesterEndDate).toDateString() + '</td>';
            
            if (sessionSemester.isCurrent) row += '<td><span class="badge badge-success">Active</span></td>';
            else row += '<td><span class="badge badge-dark">Inactive</span></td>';
            
            row += '<td><button type="button" title="Courses" class="btn btn-primary btn-xs" onclick="mapCourses(\'' + sessionSemester.id + '\')"><i class="fa fa-book"></i></button> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editSessionSemesterClick(\'' + sessionSemester.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteSessionSemesterModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSessionSemesterClick(\'' + sessionSemester.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#sessionSemesterTable tbody').append(row);
        });

        initializeDataTable($('#sessionSemesterTable'));
    }
}

function readSemestersResponse(data){
    if (data.status) {
        data.data.forEach((semester) => {
            $('#semester').append(`<option value=${semester.id}>${semester.name}</option>`);
        });
    }
}

function readSessionsResponse(data){
    if (data.status) {
        data.data.forEach((session) => {
            $('#session').append(`<option value=${session.id}>${session.name}</option>`);
        });
    }
}

function showCreateSessionSemesterButton() {
    $('#createSessionSemesterBtn').show();
    $('#editSessionSemesterBtn').hide();
    $('#createSessionSemesterTitle').show();
    $('#editSessionSemesterTitle').hide();
    
    resetField();
}

function createSessionSemester(){
    const session = $('#session').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();
    const isCurrent = $('#current').is(':checked');

    const sessionSemesterObject = {
        SessionId: session,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate,
        IsCurrent: isCurrent
    };

    api('POST', '/SessionSemester/CreateSessionSemester',
        {sessionSemester: sessionSemesterObject}, true, createSessionSemesterResponse, true, true);
}

function createSessionSemesterResponse(data){
    if (data.status) onSuccessModalHide();
}

function editSessionSemesterClick(sessionSemesterId){
    api('GET', '/SessionSemester/ReadSessionSemester',
        {sessionSemesterId: sessionSemesterId}, true, editSessionSemesterClickResponse, true);
}

function editSessionSemesterClickResponse(data){
    if (data.status){
        $('#newSessionSemesterModal').modal('show');

        $('#createSessionSemesterBtn').hide();
        $('#editSessionSemesterBtn').show();
        $('#createSessionSemesterTitle').hide();
        $('#editSessionSemesterTitle').show();

        const sessionSemester = data.data;

        $('#id').val(sessionSemester.id);
        $('#session').val(sessionSemester.sessionId);
        $('#semester').val(sessionSemester.semesterId);
        $('#startDate').val(sessionSemester.semesterStartDate);
        $('#endDate').val(sessionSemester.semesterEndDate);
        $('#current').prop('checked', sessionSemester.isCurrent);
    }
}

function editSessionSemester(){
    const sessionSemesterId = $('#id').val();

    const session = $('#session').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();
    const isCurrent = $('#current').is(':checked');
    
    const sessionSemesterObject = {
        SessionId: session,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate,
        IsCurrent: isCurrent
    };

    api('POST', '/SessionSemester/UpdateSessionSemester',
        {sessionSemesterId: sessionSemesterId, sessionSemester: sessionSemesterObject}, true, editSessionSemesterResponse, true, true);
}

function editSessionSemesterResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteSessionSemesterClick(sessionSemesterId) {
    window.sessionSemesterId = sessionSemesterId;
}

function deleteSessionSemester() {
    api('POST', '/SessionSemester/DeleteSessionSemester',
        {sessionSemesterId: window.sessionSemesterId}, true, deleteSessionSemesterResponse, true);
}

function deleteSessionSemesterResponse(data) {
    if (data.status) onSuccessModalHide();
}



function mapCourses(sessionSemesterId) {
    api('GET', '/SessionSemester/SetSessionSemesterId',
        {sessionSemesterId: sessionSemesterId}, true, mapCourseResponse, true);
}

function mapCourseResponse(data) {
    if (data.status) window.location = "/SessionSemester/SessionSemesterCourses"
}


//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newSessionSemesterModal').modal('hide');
    $('#deleteSessionSemesterModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#session').val(0);
    $('#semester').val(0);
    $('#startDate').val('');
    $('#endDate').val('');
}