//Loading Page
function StartLoading(element = 'body') {
    $(element).waitMe({
        effect: 'bounce',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255, 255, 255, 0.7)',
        color: '#000'
    });
}

function CloseLoading(element = 'body') {
    $(element).waitMe('hide');
}




//Skill Scripts
function LoadAddSkillsFormModal(id) {
    $.ajax({
        url: "/admin/skills/LoadAddSkillsFormModal",
        type: "get",
        data: {
            id: id
        },
        BeforeSend: function () {
            CloseLoading();
        },
        success: function (res) {
            CloseLoading();
            $("#modal-center-skill").html(res);

            $('#SkillForm').data('validator', null);
            $.validator.unobtrusive.parse("#SkillForm");


            $('#exampleModalmdo').modal('show');

        },
        error: function () {
            CloseLoading();
        }
    });
}

function SkillFormSubmited(_res) {
    CloseLoading();

    if (_res.status === 'Success') {
        $('#exampleModalmdo').modal('hide');
        location.reload();
    }

    else {
        alert('error')
    }
}

function DeleteSkill(id) {
    swal.fire({
        title: "اخطار",
        text: "آیا از حذف این آیتم اطمینان دارید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((willDelete) => {
        if (willDelete.value) {

            $.ajax({
                url: "/Admin/Skills/DeleteSkill",
                type: "get",
                data: {
                    id: id
                },
                beforSend: function () {
                    StartLoading();
                },
                success: function (resD) {
                    CloseLoading();

                    if (resD.isSuccess === true) {
                        swal.fire(
                            'موفق!',
                            resD.message,
                            'success'
                        );

                        $(`#ListItem-${id}`).remove();
                    } else {
                        swal.fire(
                            'خطا!',
                            resD.message,
                            'success'
                        );
                    }

                },
                error: function () {
                    CloseLoading();
                }
            });

        }
    });
}




//Education Scripts
function LoadAddEducationFormModal(id) {
    $.ajax({
        url: "/admin/Education/LoadEducationFromModal",
        type: "get",
        data: {
            id: id
        },
        BeforeSend: function () {
            CloseLoading();
        },
        success: function (res) {
            CloseLoading();
            $("#modal-center-skill").html(res);

            $('#EducationForm').data('validator', null);
            $.validator.unobtrusive.parse("#EducationForm");


            $('#exampleModalmdo').modal('show');

        },
        error: function () {
            CloseLoading();
        }
    });
}

function EducationFormSubmited(_res) {
    CloseLoading();

    if (_res.isSuccess === true) {
        
            notify.update('message', '<i class="fa fa-bell-o"></i><strong>بارگزاری</strong> داده 7877888778878787878');
         
        $('#exampleModalmdo').modal('hide');
        location.reload();
    }

    else {
        alert('error')
    }
}

function DeleteEducation(id) {
    swal.fire({
        title: "اخطار",
        text: "آیا از حذف این آیتم اطمینان دارید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((willDelete) => {
        if (willDelete.value) {

            $.ajax({
                url: "/Admin/Education/DeleteEducation",
                type: "get",
                data: {
                    id: id
                },
                beforSend: function () {
                    StartLoading();
                },
                success: function (resD) {
                    CloseLoading();

                    if (resD.isSuccess === true) {
                        swal.fire(
                            'موفق!',
                            resD.message,
                            'success'
                        );

                        $(`#ListItem-${id}`).remove();
                    } else {
                        swal.fire(
                            'خطا!',
                            resD.message,
                            'success'
                        );
                    }

                },
                error: function () {
                    CloseLoading();
                }
            });

        }
    });
}




//Portfolio Scripts
function LoadAddorEditPortfolioFormModal(id) {
    $.ajax({
        url: "/admin/Portfolio/LoadPortfolioFromModal",
        type: "get",
        contentType: 'application/x-www-form-urlencoded',
        data: {
            id: id
        },
        BeforeSend: function () {
            CloseLoading();
        },
        success: function (res) {
            CloseLoading();
            $("#modal-center-skill").html(res);

            //$('#EducationForm').data('validator', null);
            //$.validator.unobtrusive.parse("#EducationForm");


            $('#exampleModalmdo').modal('show');

        },
        error: function () {
            CloseLoading();
        }
    });
}

function PortfolioFormSubmited(_res) {
    CloseLoading();

    if (_res.isSuccess === true) {

        alert('success')

        location.reload();
    }

    else {
        alert('error')
    }
}

function DeletePortfolio(id) {
    swal.fire({
        title: "اخطار",
        text: "آیا از حذف این آیتم اطمینان دارید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((willDelete) => {
        if (willDelete.value) {

            $.ajax({
                url: "/Admin/Portfolio/DeletePortfolio",
                type: "get",
                data: {
                    id: id
                },
                beforSend: function () {
                    StartLoading();
                },
                success: function (resD) {
                    CloseLoading();

                    if (resD.isSuccess === true) {
                        swal.fire(
                            'موفق!',
                            resD.message,
                            'success'
                        );

                        $(`#ListItem-${id}`).remove();
                    } else {
                        swal.fire(
                            'خطا!',
                            resD.message,
                            'success'
                        );
                    }

                },
                error: function () {
                    CloseLoading();
                }
            });

        }
    });
}



//ThingIdo Scripts
function LoadAddThingIdoFormModal(id) {
    $.ajax({
        url: "/admin/ThingIdo/LoadThingIdoFromModal",
        type: "get",
        data: {
            id: id
        },
        BeforeSend: function () {
            CloseLoading();
        },
        success: function (res) {
            CloseLoading();
            $("#modal-center-skill").html(res);

            $('#EducationForm').data('validator', null);
            $.validator.unobtrusive.parse("#EducationForm");


            $('#exampleModalmdo').modal('show');

        },
        error: function () {
            CloseLoading();
        }
    });
}

function ThingIdoFormSubmited(_res) {
    CloseLoading();

    if (_res.isSuccess === true) {

        notify.update('message', '<i class="fa fa-bell-o"></i><strong>بارگزاری</strong> داده 7877888778878787878');

        $('#exampleModalmdo').modal('hide');
        location.reload();
    }

    else {
        alert('error')
    }
}

function DeleteThingIdo(id) {
    swal.fire({
        title: "اخطار",
        text: "آیا از حذف این آیتم اطمینان دارید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((willDelete) => {
        if (willDelete.value) {

            $.ajax({
                url: "/Admin/ThingIdo/DeleteThingIdo",
                type: "get",
                data: {
                    id: id
                },
                beforSend: function () {
                    StartLoading();
                },
                success: function (resD) {
                    CloseLoading();

                    if (resD.isSuccess === true) {
                        swal.fire(
                            'موفق!',
                            resD.message,
                            'success'
                        );

                        $(`#ListItem-${id}`).remove();
                    } else {
                        swal.fire(
                            'خطا!',
                            resD.message,
                            'success'
                        );
                    }

                },
                error: function () {
                    CloseLoading();
                }
            });

        }
    });
}


function DeletePricing(id) {
    swal.fire({
        title: "اخطار",
        text: "آیا از حذف این آیتم اطمینان دارید ؟",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله ثبت نام انجام شود',
        cancelButtonText: 'خیر'
    }).then((willDelete) => {
        if (willDelete.value) {

            $.ajax({
                url: "/Admin/Pricing/DeletePricing",
                type: "get",
                data: {
                    id: id
                },
                beforSend: function () {
                    StartLoading();
                },
                success: function (resD) {
                    CloseLoading();

                    if (resD.isSuccess === true) {
                        swal.fire(
                            'موفق!',
                            resD.message,
                            'success'
                        );
                        location.reload();

                        $(`#ListItem-${id}`).remove();
                    } else {
                        swal.fire(
                            'خطا!',
                            resD.message,
                            'success'
                        );
                    }

                },
                error: function () {
                    CloseLoading();
                }
            });

        }
    });
}