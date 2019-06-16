$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/RegisterCourse/ReadRegisteredCourses",
        {sessionSemesterId: sessionSemesterId, lecturerId: lecturerId},
        true,
        readRegisterCoursesResponse, true);
}

function readRegisterCoursesResponse(data){
    resetDataTable($('#registeredCourseTable'));

    if (data.status) {
        courses = data.data;
        data.data.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.course + '</td>';
            row += '<td>' + course.courseUnit + '</td>';
            row += '<td>' + course.lecturer + '</td>';
            row += `<td><div class="custom-control custom-checkbox">
                <input type="checkbox" class="custom-control-input" id=${course.id}>
                <label class="custom-control-label" for=${course.id}></label>
                </div></td>`;
            row += '</tr>';

            $('#registeredCourseTable tbody').append(row);
        });

        initializeDataTable($('#registeredCourseTable'));
    }
}