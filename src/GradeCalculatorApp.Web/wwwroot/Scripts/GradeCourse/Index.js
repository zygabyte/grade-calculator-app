$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/GradeCourse/ReadGradedCourses",
        {sessionSemesterId: sessionSemesterId, studentId: studentId},
        true,
        readGradedCoursesResponse, true);
}

function readGradedCoursesResponse(data){
    resetDataTable($('#gradedCoursesTable'));

    if (data.status) {
        courses = data.data;
        data.data.forEach((gradedCourse, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + gradedCourse.course + '</td>';
            row += '<td>' + gradedCourse.courseUnit + '</td>';
            row += '<td>' + gradedCourse.lecturer + '</td>';
            row += '<td>' + gradedCourse.grade + '</td>';
            row += `<td>
                        <button onclick="gradedCourseClick('${gradedCourse.id}')" title="View" class="btn btn-primary btn-xs"><i class="fa fa-eye"></i></button>
                    </td>`;
            row += '</tr>';

            $('#gradedCoursesTable tbody').append(row);
        });

        initializeDataTable($('#gradedCoursesTable'));
    }
}

function gradedCourseClick(gradedCourseId) {
    api("GET",
        "/GradeCourse/SetSessionAndStudent",
        {gradedCourseId: gradedCourseId},
        true,
        gradedCourseClickResponse, true);
}

function gradedCourseClickResponse(data) {
    if (data.status) window.location = '/GradeCourse/GradeDetails';
}