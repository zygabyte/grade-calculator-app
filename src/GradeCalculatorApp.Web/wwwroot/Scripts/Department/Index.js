let departmentId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Department/ReadDepartments",
        null,
        true,
        readDepartmentsResponse, true);

    const schoolDropDownLength = $('#school > option').length;

    if(schoolDropDownLength === 1)
        api("GET",
            "/School/ReadSchools",
            null,
            true,
            readSchoolsResponse, true);

}

function readSchoolsResponse(data){
    if (data.status) {
        data.data.forEach((school) => {
            $('#school').append(`<option value=${school.id}>${school.name}</option>`);
        });
    }
}

function readDepartmentsResponse(data){
    resetDataTable($('#departmentTable'));

    if (data.status) {
        data.data.forEach((department, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + department.school + '</td>';
            row += '<td>' + department.name + '</td>';
            row += '<td>' + department.code + '</td>';
            row += '<td><button title="View" class="btn btn-primary btn-xs" onclick="viewDepartmentClick(\'\' + department.id + \'\')"><i class="fa fa-eye"></i></button> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editDepartmentClick(\'' + department.id + '\')"><i class="fa fa-pencil"></i></button> | <a title="Delete" href="#deleteDepartmentModal" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteDepartmentClick(\'' + department.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#departmentTable tbody').append(row);
        });

        initializeDataTable($('#departmentTable'));
    }
}

function showCreateDepartmentButton() {
    $('#createDepartmentBtn').show();
    $('#editDepartmentBtn').hide();
    $('#createDepartmentTitle').show();
    $('#editDepartmentTitle').hide();

    resetField();
}

function createDepartment(){
    const name = $('#name').val();
    const code = $('#code').val();
    const school = $('#school').val();
    
    const department = {
        Name: name,
        Code: code,
        SchoolId: school
    };

    api('POST', '/Department/CreateDepartment',
        {department: department}, true, createDepartmentResponse, true);
}

function createDepartmentResponse(data){
    if (data.status) onSuccessModalHide();
}

function editDepartmentClick(departmentId){
    api('GET', '/Department/ReadDepartment',
        {departmentId: departmentId}, true, editDepartmentClickResponse, true);
}

function editDepartmentClickResponse(data){
    if (data.status){
        $('#newDepartmentModal').modal('show');

        $('#createDepartmentBtn').hide();
        $('#editDepartmentBtn').show();
        $('#createDepartmentTitle').hide();
        $('#editDepartmentTitle').show();

        const department = data.data;

        $('#id').val(department.id);
        $('#name').val(department.name);
        $('#code').val(department.code);
        $('#school').val(department.schoolId);
    }
}

function editDepartment(){
    const departmentId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();
    const school = $('#school').val();

    const department = {
        Name: name,
        Code: code,
        SchoolId: school
    };
    
    api('POST', '/Department/UpdateDepartment',
        {departmentId: departmentId, department: department}, true, editDepartmentResponse, true);
}

function editDepartmentResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteDepartmentClick(departmentId) {
    window.departmentId = departmentId;
}

function deleteDepartment() {
    api('POST', '/Department/DeleteDepartment',
        {departmentId: window.departmentId}, true, deleteDepartmentResponse, true);
}

function deleteDepartmentResponse(data) {
    if (data.status) onSuccessModalHide();
}




//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newDepartmentModal').modal('hide');
    $('#deleteDepartmentModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
    $('#school').val(0);
}