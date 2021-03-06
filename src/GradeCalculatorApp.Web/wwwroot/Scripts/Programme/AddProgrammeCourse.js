let courseIds = [];

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/ProgrammeCourse/ReadUniqueProgrammeCourses",
        {programmeId: programmeId},
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
            row += `<td><div class="custom-control custom-checkbox">
                <input type="checkbox" class="custom-control-input" id=${course.id}>
                <label class="custom-control-label" for=${course.id}></label>
                </div></td>`;
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

function addCourses(){
    courseIds = [];

    $("input:checkbox").each(function(){
        let $this = $(this);

        if($this.is(":checked")){
            courseIds.push($this.attr("id"));
        }
    });

    api("POST",
        "/ProgrammeCourse/MapCourses",
        {programmeId: programmeId, courseIds: courseIds},
        true,
        mapProgrammeCoursesResponse, true);
}

function mapProgrammeCoursesResponse(data) {
    if (data.status) window.location = "/Programme/ProgrammeCourses";
}