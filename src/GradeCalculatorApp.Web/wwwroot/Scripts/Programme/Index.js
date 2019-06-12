let programmeId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Programme/ReadProgrammes",
        null,
        true,
        readProgrammesResponse, true);

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

function readProgrammesResponse(data){
    resetDataTable($('#programmeTable'));

    if (data.status) {
        data.data.forEach((programme, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + programme.department + '</td>';
            row += '<td>' + programme.name + '</td>';
            row += '<td>' + programme.code + '</td>';
            row += '<td><button type="button" title="Courses" class="btn btn-primary btn-xs" onclick="mapCourses(\'' + programme.id + '\')"><i class="fa fa-book"></i></button> | <button title="View" class="btn btn-primary btn-xs" onclick="viewProgrammeClick(\'\' + programme.id + \'\')"><i class="fa fa-eye"></i></button> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editProgrammeClick(\'' + programme.id + '\')"><i class="fa fa-pencil"></i></button> | <a title="Delete" href="#deleteProgrammeModal" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteProgrammeClick(\'' + programme.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#programmeTable tbody').append(row);
        });

        initializeDataTable($('#programmeTable'));
    }
}

function showCreateProgrammeButton() {
    $('#createProgrammeBtn').show();
    $('#editProgrammeBtn').hide();
    $('#createProgrammeTitle').show();
    $('#editProgrammeTitle').hide();

    resetField();
}

function createProgramme(){
    const name = $('#name').val();
    const code = $('#code').val();
    const department = $('#department').val();

    const programme = {
        Name: name,
        Code: code,
        DepartmentId: department
    };

    api('POST', '/Programme/CreateProgramme',
        {programme: programme}, true, createProgrammeResponse, true);
}

function createProgrammeResponse(data){
    if (data.status) onSuccessModalHide();
}

function editProgrammeClick(programmeId){
    api('GET', '/Programme/ReadProgramme',
        {programmeId: programmeId}, true, editProgrammeClickResponse, true);
}

function editProgrammeClickResponse(data){
    if (data.status){
        $('#newProgrammeModal').modal('show');

        $('#createProgrammeBtn').hide();
        $('#editProgrammeBtn').show();
        $('#createProgrammeTitle').hide();
        $('#editProgrammeTitle').show();

        const programme = data.data;

        $('#id').val(programme.id);
        $('#name').val(programme.name);
        $('#code').val(programme.code);
        $('#department').val(programme.departmentId);
    }
}

function editProgramme(){
    const programmeId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();
    const department = $('#department').val();

    const programme = {
        Name: name,
        Code: code,
        DepartmentId: department
    };

    api('POST', '/Programme/UpdateProgramme',
        {programmeId: programmeId, programme: programme}, true, editProgrammeResponse, true);
}

function editProgrammeResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteProgrammeClick(programmeId) {
    window.programmeId = programmeId;
}

function deleteProgramme() {
    api('POST', '/Programme/DeleteProgramme',
        {programmeId: window.programmeId}, true, deleteProgrammeResponse, true);
}

function deleteProgrammeResponse(data) {
    if (data.status) onSuccessModalHide();
}

function mapCourses(programmeId) {
    api('GET', '/Programme/SetProgrammeId',
        {programmeId: programmeId}, true, mapCourseResponse, true);
}

function mapCourseResponse(data) {
    if (data.status) window.location = "/Programme/ProgrammeCourses"
}



//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newProgrammeModal').modal('hide');
    $('#deleteProgrammeModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
    $('#department').val(0);
}