let sessionSemesterCourseId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/SessionCourse/ReadSessionCourse",
        {sessionSemesterId: sessionSemesterId},
        true,
        readSessionCourseResponse, true);
}

function readSessionCourseResponse(data){
    resetDataTable($('#courseTable'));
    
    if (data.status) {
        data.data.courses.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.name + '</td>';
            
            row += '<td>' + course.code + '</td>';
            row += '<td>' + course.creditUnit + '</td>';
            row += `<td><a href="#deleteSessionSemesterCourseModal" title="Remove" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteSessionSemesterCourseClick('${course.id}')"><i class="fa fa-trash-o"></i></a></td>`;
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

function deleteSessionSemesterCourseClick(courseId) {
    sessionSemesterCourseId = courseId;
}

function deleteSessionSemesterCourse() {
    api('GET', '/SessionCourse/DeleteSessionCourse',
        {sessionSemesterId: sessionSemesterId, courseId: sessionSemesterCourseId}, true, deleteSessionSemesterCourseResponse, true);
}

function deleteSessionSemesterCourseResponse(data) {
    if (data.status) onSuccess();
}


//______________________________________UTILITIES______________________________________
function onSuccess() {
    pageLoad();

    $('#deleteSessionSemesterCourseModal').modal('hide');
}