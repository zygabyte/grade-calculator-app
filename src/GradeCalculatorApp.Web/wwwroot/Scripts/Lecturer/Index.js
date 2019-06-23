let lecturerId;

$(document).ready(function () {
    pageLoad();

    $('#downloadLecturerTemplate').click(function(e) {
        e.preventDefault();  //stop the browser from following
        window.location.href = '/Lecturer/DownloadLecturerTemplate';
    });

    $('#lecturersUpload').on('change', uploadLecturers);
});

function pageLoad() {
    api("GET",
        "/Lecturer/ReadLecturers",
        null,
        true,
        readLecturersResponse, true);

    const departmentDropDownLength = $('#department > option').length;

    if(departmentDropDownLength === 1)
        api("GET",
            "/Department/ReadDepartments",
            null,
            true,
            readDepartmentsResponse, true);

}

function readDepartmentsResponse(data){
    if (data.status) {
        data.data.forEach((department) => {
            $('#department').append(`<option value=${department.id}>${department.name}</option>`);
        });
    }
}

function readLecturersResponse(data){
    resetDataTable($('#lecturerTable'));

    if (data.status) {
        data.data.forEach((lecturer, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + lecturer.firstName + '</td>';
            row += '<td>' + lecturer.lastName + '</td>';
            row += '<td>' + lecturer.email + '</td>';
            row += '<td>' + lecturer.department + '</td>';
            row += '<td><button type="button" title="Courses" class="btn btn-primary btn-xs" onclick="mapCourses(\'' + lecturer.id + '\')"><i class="fa fa-book"></i></button> | <a href="#" title="View" class="btn btn-primary btn-xs" onclick="viewLecturerClick(\'\' + lecturer.id + \'\')"><i class="fa fa-eye"></i></a> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editLecturerClick(\'' + lecturer.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteLecturerModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteLecturerClick(\'' + lecturer.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#lecturerTable tbody').append(row);
        });

        initializeDataTable($('#lecturerTable'));
    }
}

function showCreateLecturerButton() {
    $('#createLecturerBtn').show();
    $('#editLecturerBtn').hide();
    $('#createLecturerTitle').show();
    $('#editLecturerTitle').hide();

    resetField();
}

function createLecturer(){
    const firstname = $('#firstname').val();
    const lastname = $('#lastname').val();
    const email = $('#email').val();
    const department = $('#department').val();

    const lecturer = {
        FirstName: firstname,
        LastName: lastname,
        Email: email,
        DepartmentId: department
    };

    api('POST', '/Lecturer/CreateLecturer',
        {lecturer: lecturer}, true, createLecturerResponse, true);
}

function createLecturerResponse(data){
    if (data.status) onSuccessModalHide();
}

function editLecturerClick(lecturerId){
    api('GET', '/Lecturer/ReadLecturer',
        {lecturerId: lecturerId}, true, editLecturerClickResponse, true);
}

function editLecturerClickResponse(data){
    if (data.status){
        $('#newLecturerModal').modal('show');

        $('#createLecturerBtn').hide();
        $('#editLecturerBtn').show();
        $('#createLecturerTitle').hide();
        $('#editLecturerTitle').show();

        const lecturer = data.data;

        $('#id').val(lecturer.id);
        $('#firstname').val(lecturer.firstName);
        $('#lastname').val(lecturer.lastName);
        $('#email').val(lecturer.email);
        $('#department').val(lecturer.departmentId);
    }
}

function editLecturer(){
    const lecturerId = $('#id').val();

    const firstname = $('#firstname').val();
    const lastname = $('#lastname').val();
    const email = $('#email').val();
    const department = $('#department').val();

    const lecturer = {
        FirstName: firstname,
        LastName: lastname,
        Email: email,
        DepartmentId: department
    };

    api('POST', '/Lecturer/UpdateLecturer',
        {lecturerId: lecturerId, lecturer: lecturer}, true, editLecturerResponse, true);
}

function editLecturerResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteLecturerClick(lecturerId) {
    window.lecturerId = lecturerId;
}

function deleteLecturer() {
    api('POST', '/Lecturer/DeleteLecturer',
        {lecturerId: window.lecturerId}, true, deleteLecturerResponse, true);
}

function deleteLecturerResponse(data) {
    if (data.status) onSuccessModalHide();
}

function mapCourses(lecturerId) {
    api('GET', '/Lecturer/SetLecturerId',
        {lecturerId: lecturerId}, true, mapCourseResponse, true);
}

function mapCourseResponse(data) {
    if (data.status) window.location = "/Lecturer/LecturerCourses"
}

function uploadLecturers() {
    const fileInput = document.getElementById('lecturersUpload');

    const data = fileInput.files[0];

    const formData = new FormData();
    formData.append(data.name, data);

    apiFile("POST",
        "/Lecturer/UploadLecturers",
        formData,
        true,
        uploadLecturersResponse,
        true, true);
}

function uploadLecturersResponse(data) {
    if (data.status) {
        toastr.success('Successfully uploaded lecturers', 'Success');
        pageLoad();
    }
}
//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newLecturerModal').modal('hide');
    $('#deleteLecturerModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#firstname').val('');
    $('#lastname').val('');
    $('#email').val('');
    $('#department').val(0);
}