$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/RegisterCourse/ReadRegisteredCoursesByStudent",
        {sessionSemesterId: sessionSemesterId, studentId: studentId},
        true,
        readRegisterCoursesResponse, true);
}

function readRegisterCoursesResponse(data){
    resetDataTable($('#registeredCourseTable'));

    if (data.status) {
        data.data.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.course + '</td>';
            row += '<td>' + course.courseCode + '</td>';
            row += '<td>' + course.courseCredit + '</td>';
            row += '<td>' + course.lecturer + '</td>';
            row += '</tr>';

            $('#registeredCourseTable tbody').append(row);
        });

        initializeDataTable($('#registeredCourseTable'));
    }
}