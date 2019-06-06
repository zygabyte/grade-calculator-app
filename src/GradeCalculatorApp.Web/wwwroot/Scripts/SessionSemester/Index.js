$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/SessionSemester/ReadSessionSemesters",
        null,
        true,
        readSessionSemestersResponse, true);
    
    api("GET",
        "/Semester/ReadSemesters",
        null,
        true,
        readSemestersResponse, true);
}

function readSessionSemestersResponse(data) {
    console.log('sesion data');
    console.log(data);
    resetDataTable($('#sessionSemesterTable'));
    
    if (data.status) {
        data.data.forEach((sessionSemester, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + sessionSemester.name + '</td>';
            row += '<td>' + sessionSemester.semester + '</td>';
            row += '<td>' + new Date(sessionSemester.semesterStartDate).toDateString() + '</td>';
            row += '<td>' + new Date(sessionSemester.semesterEndDate).toDateString() + '</td>';
            row += '<td> active </td>';
            row += '<td><button type="button" class="btn btn-primary btn-xs" onclick="mapCourses(\'' + sessionSemester.id + '\')">Courses</button> | <button type="button" class="btn btn-success btn-xs" onclick="editSessionSemesterClick(\'' + sessionSemester.id + '\')">Edit</button> | <a href="#deleteSessionSemesterModal" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSessionSemesterClick(\'' + sessionSemester.id + '\')">Delete</a></td>';
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

function showCreateSessionSemesterButton() {
    $('#createSessionSemesterBtn').show();
    $('#editSessionSemesterBtn').hide();
    $('#createSessionSemesterTitle').show();
    $('#editSessionSemesterTitle').hide();
    
    resetField();
}

function createSessionSemester(){
    const sessionSemester = $('#sessionSemester').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();

    const sessionSemesterObject = {
        Name: sessionSemester,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate
    };

    api('POST', '/SessionSemester/CreateSessionSemester',
        {sessionSemester: sessionSemesterObject}, true, createSessionSemesterResponse, true);
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
        $('#sessionSemester').val(sessionSemester.name);
        $('#semester').val(sessionSemester.semesterId);
        $('#startDate').val(sessionSemester.semesterStartDate);
        $('#endDate').val(sessionSemester.semesterEndDate);
    }
}

function editSessionSemester(){
    const sessionSemesterId = $('#id').val();

    const sessionSemester = $('#sessionSemester').val();
    const semester = $('#semester').val();
    const semesterStartDate = $('#startDate').val();
    const semesterEndDate = $('#endDate').val();

    const sessionSemesterObject = {
        Name: sessionSemester,
        SemesterId: semester,
        SemesterStartDate: semesterStartDate,
        SemesterEndDate: semesterEndDate
    };

    api('POST', '/SessionSemester/UpdateSessionSemester',
        {sessionSemesterId: sessionSemesterId, sessionSemester: sessionSemesterObject}, true, editSessionSemesterResponse, true);
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



//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newSessionSemesterModal').modal('hide');
    $('#deleteSessionSemesterModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#sessionSemester').val('');
    $('#semester').val(0);
    $('#startDate').val('');
    $('#endDate').val('');
}