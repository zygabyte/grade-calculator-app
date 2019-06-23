let studentId;

$(document).ready(function () {
    pageLoad();

    $('#downloadStudentTemplate').click(function(e) {
        e.preventDefault();  //stop the browser from following
        window.location.href = '/Student/DownloadStudentTemplate';
    });

    $('#studentsUpload').on('change', uploadStudents);
});

function pageLoad() {
    api("GET",
        "/Student/ReadStudents",
        null,
        true,
        readStudentsResponse, true);

    const programmeDropDownLength = $('#programme > option').length;

    if(programmeDropDownLength === 1)
        api("GET",
            "/Programme/ReadProgrammes",
            null,
            true,
            readProgrammesResponse, true);

}

function readProgrammesResponse(data){
    if (data.status) {
        data.data.forEach((programme) => {
            $('#programme').append(`<option value=${programme.id}>${programme.name}</option>`);
        });
    }
}

function readStudentsResponse(data){
    resetDataTable($('#studentTable'));

    if (data.status) {
        data.data.forEach((student, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + student.firstName + '</td>';
            row += '<td>' + student.lastName + '</td>';
            row += '<td>' + student.email + '</td>';
            row += '<td>' + student.matricNumber + '</td>';
            row += '<td>' + student.programme + '</td>';
            row += '<td><a href="#" title="View" class="btn btn-primary btn-xs" onclick="viewStudentClick(\'\' + student.id + \'\')"><i class="fa fa-eye"></i></a> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editStudentClick(\'' + student.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteStudentModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteStudentClick(\'' + student.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#studentTable tbody').append(row);
        });

        initializeDataTable($('#studentTable'));
    }
}

function showCreateStudentButton() {
    $('#createStudentBtn').show();
    $('#editStudentBtn').hide();
    $('#createStudentTitle').show();
    $('#editStudentTitle').hide();

    resetField();
}

function createStudent(){
    const firstname = $('#firstname').val();
    const lastname = $('#lastname').val();
    const email = $('#email').val();
    const matric = $('#matric').val();
    const programme = $('#programme').val();

    const student = {
        FirstName: firstname,
        LastName: lastname,
        Email: email,
        ProgrammeId: programme,
        MatricNumber: matric
    };

    api('POST', '/Student/CreateStudent',
        {student: student}, true, createStudentResponse, true);
}

function createStudentResponse(data){
    if (data.status) onSuccessModalHide();
}

function editStudentClick(studentId){
    api('GET', '/Student/ReadStudent',
        {studentId: studentId}, true, editStudentClickResponse, true);
}

function editStudentClickResponse(data){
    if (data.status){
        $('#newStudentModal').modal('show');

        $('#createStudentBtn').hide();
        $('#editStudentBtn').show();
        $('#createStudentTitle').hide();
        $('#editStudentTitle').show();

        const student = data.data;

        $('#id').val(student.id);
        $('#firstname').val(student.firstName);
        $('#lastname').val(student.lastName);
        $('#email').val(student.email);
        $('#matric').val(student.matric);
        $('#programme').val(student.programmeId);
    }
}

function editStudent(){
    const studentId = $('#id').val();

    const firstname = $('#firstname').val();
    const lastname = $('#lastname').val();
    const email = $('#email').val();
    const matric = $('#matric').val();
    const programme = $('#programme').val();

    const student = {
        FirstName: firstname,
        LastName: lastname,
        Email: email,
        ProgrammeId: programme,
        MatricNumber: matric
    };

    api('POST', '/Student/UpdateStudent',
        {studentId: studentId, student: student}, true, editStudentResponse, true);
}

function editStudentResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteStudentClick(studentId) {
    window.studentId = studentId;
}

function deleteStudent() {
    api('POST', '/Student/DeleteStudent',
        {studentId: window.studentId}, true, deleteStudentResponse, true);
}

function deleteStudentResponse(data) {
    if (data.status) onSuccessModalHide();
}

function uploadStudents() {
    const fileInput = document.getElementById('studentsUpload');

    const data = fileInput.files[0];

    const formData = new FormData();
    formData.append(data.name, data);

    apiFile("POST",
        "/Student/UploadStudents",
        formData,
        true,
        uploadStudentsResponse,
        true, true);
}

function uploadStudentsResponse(data) {
    if (data.status) {
        toastr.success('Successfully uploaded students', 'Success');
        pageLoad();
    }
}



//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newStudentModal').modal('hide');
    $('#deleteStudentModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#firstname').val('');
    $('#lastname').val('');
    $('#email').val('');
    $('#programme').val(0);
}