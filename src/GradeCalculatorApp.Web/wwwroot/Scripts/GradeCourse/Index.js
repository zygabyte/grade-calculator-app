let registeredCourseId;

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
                        <a href="#" title="View" class="btn btn-primary btn-xs"><i class="fa fa-eye"></i></a>
                    </td>`;
            row += '</tr>';

            $('#gradedCoursesTable tbody').append(row);
        });

        initializeDataTable($('#gradedCoursesTable'));
    }
}

function gradeStudentClick(registeredCourseId) {
    window.registeredCourseId = registeredCourseId;
}

function createRegisteredCourseGrade() {
    const quiz1 = $('#quiz1').val();
    const quiz2 = $('#quiz2').val();
    const assignment1 = $('#assignment1').val();
    const assignment2 = $('#assignment2').val();
    const attendance = $('#attendance').val();
    const midSemester = $('#mid_semester').val();
    const project = $('#project').val();
    const exam = $('#exam').val();

    const registeredCourseGrade = {
        Quiz1: quiz1, Quiz2: quiz2,
        Assignment1: assignment1, Assignment2: assignment2,
        Attendance: attendance, MidSemester: midSemester,
        Project: project, Exam: exam,
        RegisteredCourseId: window.registeredCourseId
    };

    api('POST', '/RegisteredCourseGrade/CreateRegisteredCourseGrade',
        {registeredCourseGrade: registeredCourseGrade}, true, registeredCourseGradeResponse, true);
}

function registeredCourseGradeResponse(data) {
    if (data.status) onSuccessModalHide()
}


//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#gradeModal').modal('hide');
}

function resetField() {
    $('#quiz1').val('');
    $('#quiz2').val('');
    $('#assignment1').val('');
    $('#assignment2').val('');
    $('#attendance').val('');
    $('#mid_semester').val('');
    $('#project').val('');
    $('#exam').val('');
}