let schoolId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/School/ReadSchools",
        null,
        true,
        readSchoolsResponse, true);

}

function readSchoolsResponse(data){
    resetDataTable($('#schoolTable'));

    if (data.status) {
        data.data.forEach((school, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + school.name + '</td>';
            row += '<td>' + school.code + '</td>';
            row += '<td><button type="button" class="btn btn-success btn-xs" onclick="editSchoolClick(\'' + school.id + '\')">Edit</button> | <a href="#deleteSchoolModal" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSchoolClick(\'' + school.id + '\')">Delete</a></td>';
            row += '</tr>';

            $('#schoolTable tbody').append(row);
        });

        initializeDataTable($('#schoolTable'));
    }
}

function showCreateSchoolButton() {
    $('#createSchoolBtn').show();
    $('#editSchoolBtn').hide();
    $('#createSchoolTitle').show();
    $('#editSchoolTitle').hide();

    resetField();
}

function createSchool(){
    const name = $('#name').val();
    const code = $('#code').val();

    const school = {
        Name: name,
        Code: code
    };

    api('POST', '/School/CreateSchool',
        {school: school}, true, createSchoolResponse, true);
}

function createSchoolResponse(data){
    if (data.status) onSuccessModalHide();
}

function editSchoolClick(schoolId){
    api('GET', '/School/ReadSchool',
        {schoolId: schoolId}, true, editSchoolClickResponse, true);
}

function editSchoolClickResponse(data){
    if (data.status){
        $('#newSchoolModal').modal('show');

        $('#createSchoolBtn').hide();
        $('#editSchoolBtn').show();
        $('#createSchoolTitle').hide();
        $('#editSchoolTitle').show();

        const school = data.data;

        $('#id').val(school.id);
        $('#name').val(school.name);
        $('#code').val(school.code);
    }
}

function editSchool(){
    const schoolId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();

    const school = {
        Name: name,
        Code: code
    };

    api('POST', '/School/UpdateSchool',
        {schoolId: schoolId, school: school}, true, editSchoolResponse, true);
}

function editSchoolResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteSchoolClick(schoolId) {
    window.schoolId = schoolId;
}

function deleteSchool() {
    api('POST', '/School/DeleteSchool',
        {schoolId: window.schoolId}, true, deleteSchoolResponse, true);
}

function deleteSchoolResponse(data) {
    if (data.status) onSuccessModalHide();
}




//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newSchoolModal').modal('hide');
    $('#deleteSchoolModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
}