$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/GradeCourse/ReadGradedCourse",
        {gradedCourseId: gradedCourseId},
        true,
        readGradedCourseResponse, true);
}

function readGradedCourseResponse(data) {
    if (data.status){
        resetDataTable($('#gradeDetailsTable'));

        const gradedCourse = data.data;
        let row;

        $('#exam').html(gradedCourse.exam);
        $('#totalCa').html(gradedCourse.totalCa);
        $('#finalScore').html(gradedCourse.finalScore);
        $('#grade').html(gradedCourse.grade);
        
        row = `<tr><td>1</td><td>Quiz 1</td><td>${gradedCourse.quiz1}</td></tr>`;
        row += `<tr><td>2</td><td>Quiz 2</td><td>${gradedCourse.quiz2}</td></tr>`;
        row += `<tr><td>3</td><td>Assignment 1</td><td>${gradedCourse.assignment1}</td></tr>`;
        row += `<tr><td>4</td><td>Assignment 2</td><td>${gradedCourse.assignment2}</td></tr>`;
        row += `<tr><td>5</td><td>Attendance</td><td>${gradedCourse.attendance}</td></tr>`;
        row += `<tr><td>6</td><td>Project</td><td>${gradedCourse.project}</td></tr>`;
        row += `<tr><td>7</td><td>Mid Semester</td><td>${gradedCourse.midSemester}</td></tr>`;

        $('#gradeDetailsTable tbody').append(row);

        initializeDataTable($('#gradeDetailsTable'));
        
        
    }
}