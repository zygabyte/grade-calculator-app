let semesterId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Semester/ReadSemesters",
        null,
        true,
        readSemestersResponse, true);
    
}

function readSemestersResponse(data){
    resetDataTable($('#semesterTable'));

    if (data.status) {
        data.data.forEach((semester, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + semester.name + '</td>';
            row += '<td>' + semester.code + '</td>';
            row += '<td><button title="Edit" type="button" class="btn btn-success btn-xs" onclick="editSemesterClick(\'' + semester.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteSemesterModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSemesterClick(\'' + semester.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#semesterTable tbody').append(row);
        });

        initializeDataTable($('#semesterTable'));
    }
}

function showCreateSemesterButton() {
    $('#createSemesterBtn').show();
    $('#editSemesterBtn').hide();
    $('#createSemesterTitle').show();
    $('#editSemesterTitle').hide();

    resetField();
}

function createSemester(){
    const name = $('#name').val();
    const code = $('#code').val();
    
    const semester = {
        Name: name,
        Code: code
    };
    
    api('POST', '/Semester/CreateSemester', 
        {semester: semester}, true, createSemesterResponse, true);
}

function createSemesterResponse(data){
    if (data.status) onSuccessModalHide();
}

function editSemesterClick(semesterId){
    api('GET', '/Semester/ReadSemester',
        {semesterId: semesterId}, true, editSemesterClickResponse, true);
}

function editSemesterClickResponse(data){
    if (data.status){
        $('#newSemesterModal').modal('show');

        $('#createSemesterBtn').hide();
        $('#editSemesterBtn').show();
        $('#createSemesterTitle').hide();
        $('#editSemesterTitle').show();
        
        const semester = data.data;

        $('#id').val(semester.id);
        $('#name').val(semester.name);
        $('#code').val(semester.code);
    }
}

function editSemester(){
    const semesterId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();
    
    const semester = {
        Name: name,
        Code: code
    };
    
    api('POST', '/Semester/UpdateSemester',
        {semesterId: semesterId, semester: semester}, true, editSemesterResponse, true);
}

function editSemesterResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteSemesterClick(semesterId) {
    window.semesterId = semesterId;
}

function deleteSemester() {
    api('POST', '/Semester/DeleteSemester',
        {semesterId: window.semesterId}, true, deleteSemesterResponse, true);
}

function deleteSemesterResponse(data) {
    if (data.status) onSuccessModalHide();
}




//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newSemesterModal').modal('hide');
    $('#deleteSemesterModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
}