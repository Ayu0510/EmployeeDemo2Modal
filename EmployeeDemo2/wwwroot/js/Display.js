function paginationFunction(URL = '/Employee/GetAllEmployee') {

    if ($('#search').val() != "" || $('#search').val() != "") {
        URL = "/Employee/GetAllEmployee?SearchText=" + $('#search').val();
    }

    $.ajax({
        url: URL,
        type: "GET",
        dataType: "json",
        success: function (data) {
            $('#demo').pagination({
                dataSource: data,
                locator:'data',
                pageSize: 5,
                callback: function (data, pagination) {
                    var html = '';
                    $('#employeedisplay').empty();
                    $.each(data, function (index, item) {
                        html = `
                        <tr class="view">
                            <td><h4>${item.id}<button class="btn parent" data-child="${item.firstName}-${item.id}" type="button" id="detailstoggle" data-toggle="modal" data-target="#employeeData"><i class="bi bi-plus-circle-fill"></i></button></h4></td>
                            <td class="text-center"><img src="Image/${item.image}" height="80" width="80"> ${item.firstName} ${item.lastName}</td>
                            <td class="text-center">${item.email}</td>
                            <td class="text-center">${item.designation}</td>
                            <td class="text-center">${item.gender}</td>
                            <td class="text-center">
                                <a class = "btn text-center btn-success bi bi-pencil-square" href= "Employee/Upsert/${item.id}" type = "button"></a>
                                <button type="button" id="UpsertPartial" data-employee-id="${item.id}" class="btn text-center btn-success bi bi-pencil-square upsert" data-toggle="ajax-modal" data-target="#employeeAddAndEdit" data-url="/Employee/UpSertPartial/${item.id}">
                                modal
                                </button>
                                <button class = "btn text-center btn-danger bi bi-trash-fill" onclick = "deleteEmployeeAlert('Employee/Delete/${item.id}')" type = "button"></button>
                            </td>
                        </tr>
                         <tr class="child ${item.firstName}-${item.id} animate__animated animate__fadeInDown animate__fast hidden">
                            <td></td>
                            <td colspan="6"><h5>Date Of Birth:${item.dateOfBirth}</h5>
                            <h5>Date Of Joining:${item.dateOfJoining}</h5>
                            <h5>Skills:${item.skillNames}</h5>
                            <h5>Details:${item.description}</h5></td>
                        </tr>
                        `;
                        $('#employeedisplay').append(html);
                    });
                    let table = document.querySelectorAll('.__table')[0];

                    let parents = table.querySelectorAll('.parent');
                    // console.clear();
                    parents.forEach((p) => {

                        p.addEventListener('click', (el) => {
                            let classname = p.getAttribute("data-child");
                            let children = table.querySelectorAll('.' + classname);
                            children.forEach((c) => {
                                if (c.className.search("hidden") > -1) {
                                    c.className = c.className.replace("hidden", " ");
                                } else {
                                    c.className = c.className + ' hidden';
                                }
                            })
                        })
                    })
                }
            });
        }
    })





    //$.ajax({
    //    url: URL,
    //    type: "GET",
    //    dataType: "json",
    //    success: function (data) {
    //        var html = '';
    //        $('#employeedisplay').empty();
    //        $.each(data, function (index, item) {
    //            html = `
    //            <tr class="view">
    //                <td class="text-center"><button class="btn-light btn-outline-light rounded-5" type="button" data-bs-toggle="collapse" data-bs-target="#collapseExample-${item.id}" aria-expanded="false" aria-controls="collapseExample-${item.id}"><i class="bi bi-plus-circle-fill"></i></button></td>
    //                <td class="text-center"><img src="Image/${item.image}" height="80" width="80"> ${item.firstName} ${item.lastName}</td>
    //                <td class="text-center">${item.email}</td>
    //                <td class="text-center">${item.designation}</td>
    //                <td class="text-center">${item.gender}</td>
    //                <td class="text-center">
    //                    <a class = "btn text-center btn-success bi bi-pencil-square" href= "Employee/Upsert/${item.id}" type = "button"></a>
    //                    <button type="button" id="UpsertPartial" data-employee-id="${item.id}" class="btn text-center btn-success bi bi-pencil-square upsert" data-toggle="ajax-modal" data-target="#employeeAddAndEdit" data-url="/Employee/UpSertPartial/${item.id}">
    //                    modal
    //                    </button>
    //                    <button class = "btn text-center btn-danger bi bi-trash-fill" onclick = "deleteEmployeeAlert('Employee/Delete/${item.id}')" type = "button"></button>
    //                </td>
    //            </tr>
    //                       <tr class="fold">
    //                        <td colspan="3">
    //                            <div>DateOfBirth:  @employee.DateOfBirth</div>
    //                            <div>DateOfJoining:  @employee.DateOfJoining</div>
    //                            <div>Skills:  @employee.SkillNames</div>
    //                            <div>Description:  @employee.Description</div>
    //                        </td>
    //                    </tr>
    //            `;
    //            $('#employeedisplay').append(html);
    //        });

    //    }
    //});
}