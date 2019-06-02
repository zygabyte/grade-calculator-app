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
            row += '<td><button type="button" class="btn btn-success btn-sm" id="approve_btn" onclick="editSemester(\'' + semester.Id + '\')">Edit</button> | <button type="button" class="btn btn-danger btn-sm" onclick="deleteSemester(\'' + semester.Id + '\')">Delete</button></td>';
            row += '</tr>';

            $('#semesterTable tbody').append(row);
        });

        initializeDataTable($('#semesterTable'));
    }
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
    }
}

function resetField() {
    $('#name').val('');
    $('#code').val('');
}