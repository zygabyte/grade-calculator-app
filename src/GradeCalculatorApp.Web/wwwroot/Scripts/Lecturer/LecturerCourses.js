let lecturerCourseId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/LecturerCourse/ReadLecturerCourse",
        {lecturerId: lecturerId},
        true,
        readLecturerCourseResponse, true);
}

function readLecturerCourseResponse(data){
    resetDataTable($('#courseTable'));

    if (data.status) {
        data.data.courses.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.name + '</td>';
            row += '<td>' + course.code + '</td>';
            row += '<td>' + course.creditUnit + '</td>';
            row += `<td><a href="#deleteLecturerCourseModal" title="Remove" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteLecturerCourseClick('${course.id}')"><i class="fa fa-trash-o"></i></a></td>`;
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

function deleteLecturerCourseClick(courseId) {
    lecturerCourseId = courseId;
}

function deleteLecturerCourse() {
    api('GET', '/LecturerCourse/DeleteLecturerCourse',
        {lecturerId: lecturerId, courseId: lecturerCourseId}, true, deleteLecturerCourseResponse, true);
}

function deleteLecturerCourseResponse(data) {
    if (data.status) onSuccess();
}


//______________________________________UTILITIES______________________________________
function onSuccess() {
    pageLoad();

    $('#deleteLecturerCourseModal').modal('hide');
}