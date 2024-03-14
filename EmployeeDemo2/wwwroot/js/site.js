$(document).ready(function () {
    
    //$('#__table').DataTable({ pageLength: 5, lengthMenu:[5,10,15,20,25,30,35,40,45,50] });

    paginationFunction();
    $(document).on("click", "#UpsertPartial", function () {
        const employeeId = $(this).data('employee-id');
        const url = 'Employee/UpSertPartial/' + employeeId;
        loadModal(url);
    });

    function loadModal(url) {
        const decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            $('#PlaceHolder').html(data);
            $('#PlaceHolder').find('.modal').modal('show');
        });
    }

    $(".fold-table .view").on("click", function () {
        $(this).toggleClass("open").next(".fold").toggleClass("open");
    });
});

//$('#__table').pagination({
//    dataSource: [1, 2, 3, 4, 5, 6, 7],
//    callback: function (data, pagination) {
//        loaddata();
//        var html = template(data);
//        dataContainer.html(html);
//    }
//})

var flag = 1;
function nameSort(type) {

    if (type == 'Gender' && flag == 0) {
        paginationFunction('/Employee/GetAllEmployee?sortOrder=' + type + 'female_desc');
        flag = 1;
    }
    else if (flag == 1) {
        paginationFunction('/Employee/GetAllEmployee?sortOrder=' + type + '_desc');
        flag = 0;
    }
    else {
        paginationFunction('/Employee/GetAllEmployee');
        flag = 1;
    }
}

$(function () {

    var placeHoderElement = $('#PlaceHolder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            placeHoderElement.html(data);
            placeHoderElement.find('.modal').modal('show');
        })
    })
    placeHoderElement.on('click', '[data-save="modal"]', function (event) {
        
        let IsValid = Validate("Add-employee");
        if (IsValid == true) {
            var form = $(this).parents(".modal").find('#myForm');
            var actionurl = $('#myForm').attr('action')
            var sendData = new FormData(form[0]);

            $.ajax({
                url: actionurl,
                type: "POST",
                data: sendData,
                processData: false,
                contentType: false,
                success: function (data) {
                    placeHoderElement.find(".modal").modal("hide");
                    pagination();
                }
            })
        }
        else {
            placeHoderElement.find(".modal").modal("show");
        }
    })
})

function skillArray() {
    return skillsArray = []
}
    $("#search").keydown(function (e) {
        let input = $("#search").val().toUpperCase();
        let filter = $("#searchSelect").val();
        let table = $("#__table");

        $("tr").each(function () {
            let tdIndex;
            switch (filter) {
                case 'Name':
                    tdIndex = 1;
                    break;
                case 'Email':
                    tdIndex = 2;
                    break;
                case 'Designation':
                    tdIndex = 3;
                    break;
                case 'Gender':
                    tdIndex = 4;
                    break;
                default:
                    tdIndex = 1; // Default to the first column if no match
            }

            let td = $(this).find("td").eq(tdIndex);
            let txtValue = td.text().toUpperCase();
            if (input.length >= 2) {
                if (txtValue.indexOf(input) > -1) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            }
            else {
                display();
            }
        });
    });