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
            row += '<td><button type="button" class="btn btn-success btn-sm" onclick="editSemesterClick(\'' + semester.id + '\')">Edit</button> | <button type="button" class="btn btn-danger btn-sm" onclick="deleteSemester(\'' + semester.id + '\')">Delete</button></td>';
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
    if (data.status){
        pageLoad();
        resetField();

        $('#newSemesterModal').modal('hide');
    }
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
    if (data.status){
        pageLoad();
        resetField();

        $('#newSemesterModal').modal('hide');
    }
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
}