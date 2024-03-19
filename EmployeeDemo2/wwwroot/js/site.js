function displayLoader() {
    $('.loader').show();
}
function hideLoader() {
    $('.loader').hide();
}
$(window).on("beforeunload", function () {
    displayLoader();
})

$(document).on('submit', function () {
    displayLoader();
})
window.setTimeout(function () {
    hideLoader();
}, 1000)

function clickMinus(x) {
    if (x.innerHTML === '<i id="plusMinus" class="fa fa-plus" aria-hidden="true"></i>') {
        x.innerHTML = '<i id="plusMinus" class="fa fa-minus" aria-hidden="true"></i>';
    } else {
        x.innerHTML = '<i id="plusMinus" class="fa fa-plus" aria-hidden="true"></i>';
    }
}


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
            skillArray();
            $('#PlaceHolder').html(data);
            $('#PlaceHolder').find('.modal').modal('show');  
        });
    }
});
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
    //$('button[data-toggle="ajax-modal"]').click(function (event) {
    //    var url = $(this).data('url');
    //    var decodedUrl = decodeURIComponent(url);
    //    $.get(decodedUrl).done(function (data) {
    //        skillArray();
    //        placeHoderElement.html(data);
    //        placeHoderElement.find('.modal').modal('show');
    //    })
    //})
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
                    paginationFunction();
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