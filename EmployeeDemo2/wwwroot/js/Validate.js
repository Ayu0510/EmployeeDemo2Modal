function Validate(employeeClassname) {
    
    let isValid = true;
    const inputs = $("." + employeeClassname).find(".nat-required");
    const inputemail = $("." + employeeClassname).find(".nat-email");

    $(".error").remove("span.error");

    inputs.each(function (index, inputs) {
        if (inputs.type == "text" || inputs.type == "textarea" || inputs.type == "select-one" || inputs.type == "date" || inputs.type == "email") {
            if ((inputs.value) == "" || (inputs.value) == "Select") {
                isValid = false;

                setErrorMsg(inputs, ShowHideError);
                registerKeyUpChangeEvent(inputs);
            }
            else {
                setErrorMsg(inputs, ShowHideError);
                registerKeyUpChangeEvent(inputs);
            }
        }
    });

    inputemail.each(function (index, inputemail) {
        if (inputemail.type == "email") {
            $(inputemail).next().remove("span.error");
            let validemail = emailValid(inputemail);
            if ((inputemail.value) != "" && validemail == true) {
                setErrorMsg(inputemail, ShowHideError);
                registerKeyUpChangeEventOnEmail(inputemail);
            } else {
                isValid = false;
                setErrorMsg(inputemail, ShowHideError);
                registerKeyUpChangeEventOnEmail(inputemail);
            }
        }
    });
    return isValid;

}

function setErrorMsg(input, callBack) {
    callBack(input);
}

function ShowHideError(input) {
    
    const errorMessage = $(input).attr("data-val-required");
    if (errorMessage != null) {
        $(input).after("<span class='error'>" + errorMessage + "</span>").show();
    }
    else {
        $(input).next().find(".error").remove();
    }
};

function registerKeyUpChangeEvent(input) {
    
    $(input).keyup(function () {
        eventMessage(input);
    });
    $(input).change(function () {
        eventMessage(input);
    });
}

function eventMessage(input) {
    
    const errorMessage = $(input).attr("data-val-required");
    $(input).next().remove("span.error");
    if ($(input).val() === "" || $(input).val() === "Select") {
        $(input).after(`<span class="error">${errorMessage}</span>`).show();
    } else {
        $(input).next().remove(".error");
    }
}

function registerKeyUpChangeEventOnEmail(input) {
    $(input).keyup(function () {
        const validemail = emailValid(input)
        $(input).next().remove("span.error");
        const errorMessage = $(input).attr("validMessage");
        if (validemail == true) {
            $(input).next().remove("span.error");
        }
        else {
            $(input).after("<span class='error'>" + errorMessage + "</span>").show();
        }
    });
}

function emailValid(input) {
    if ($(input).val().match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)) {
        return true;
    }
    else {
        return false;
    }
}