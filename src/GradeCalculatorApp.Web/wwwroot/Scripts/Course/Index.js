let courseId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/Course/ReadCourses",
        null,
        true,
        readCoursesResponse, true);
}

function readCoursesResponse(data){
    resetDataTable($('#courseTable'));

    if (data.status) {
        data.data.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.name + '</td>';
            row += '<td>' + course.code + '</td>';
            row += '<td>' + course.creditUnit + '</td>';
            row += '<td><a href="#" title="View" class="btn btn-primary btn-xs" onclick="viewCourseClick(\'\' + course.id + \'\')"><i class="fa fa-eye"></i></a> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editCourseClick(\'' + course.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteCourseModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteCourseClick(\'' + course.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

function showCreateCourseButton() {
    $('#createCourseBtn').show();
    $('#editCourseBtn').hide();
    $('#createCourseTitle').show();
    $('#editCourseTitle').hide();

    resetField();
}

function createCourse(){
    const name = $('#name').val();
    const code = $('#code').val();
    const creditUnit = $('#credit_unit').val();

    const course = {
        Name: name,
        Code: code,
        CreditUnit: creditUnit
    };

    api('POST', '/Course/CreateCourse',
        {course: course}, true, createCourseResponse, true);
}

function createCourseResponse(data){
    if (data.status) onSuccessModalHide();
}

function editCourseClick(courseId){
    api('GET', '/Course/ReadCourse',
        {courseId: courseId}, true, editCourseClickResponse, true);
}

function editCourseClickResponse(data){
    if (data.status){
        $('#newCourseModal').modal('show');

        $('#createCourseBtn').hide();
        $('#editCourseBtn').show();
        $('#createCourseTitle').hide();
        $('#editCourseTitle').show();

        const course = data.data;

        $('#id').val(course.id);
        $('#name').val(course.name);
        $('#code').val(course.code);
        $('#credit_unit').val(course.creditUnit);
    }
}

function editCourse(){
    const courseId = $('#id').val();

    const name = $('#name').val();
    const code = $('#code').val();
    const creditUnit = $('#credit_unit').val();

    const course = {
        Name: name,
        Code: code,
        CreditUnit: creditUnit
    };

    api('POST', '/Course/UpdateCourse',
        {courseId: courseId, course: course}, true, editCourseResponse, true);
}

function editCourseResponse(data) {
    if (data.status) onSuccessModalHide();
}

function deleteCourseClick(courseId) {
    window.courseId = courseId;
}

function deleteCourse() {
    api('POST', '/Course/DeleteCourse',
        {courseId: window.courseId}, true, deleteCourseResponse, true);
}

function deleteCourseResponse(data) {
    if (data.status) onSuccessModalHide();
}




//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#newCourseModal').modal('hide');
    $('#deleteCourseModal').modal('hide');
}

function resetField() {
    $('#id').val('');
    $('#name').val('');
    $('#code').val('');
    $('#credit_unit').val('');
}