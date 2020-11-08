$(document).ready(function() {

    


    $('#closemodal').click(function () {

        setTimeout(() => {
            $("#form-modal .modal-body").html("");
            $("#form-modal .modal-title").html("");

        }, 250);

    })


    refreshProductTable = () => {

        $.ajax({
            type: "GET",
            url: "Product/GetProductsViewComponent",
            success: function (res) {
                $('#productTable').html(res);
            }
        })
    }
   


    showAddProductInModal = (url, title) => {

       
        $.ajax({
            type: "GET",
            url: url,
            success: function (res) {
                $("#form-modal .modal-body").html(res);
                $("#form-modal .modal-title").html(title);
                $('#form-modal').modal('show');
            }
        })
         
    }


    AjaxAddProduct = form => {

        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {

                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshProductTable();
                    $('#closemodal').trigger("click");

                }
                else {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    })

                }
                }
        })

        return false;
    }


    showUpdateProductInModal = (url,title) => {

        $.ajax({
            type: "GET",
            url: url,
            success : function(res) {

                if (res.success === false) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message
                    });
                }

                else {

                    $("#form-modal .modal-body").html(res);
                    $("#form-modal .modal-title").html(title);
                    $('#form-modal').modal('show');
                }
            }
        })

    }



    AjaxUpdateProduct = form => {

        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {

                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshProductTable();
                    $('#closemodal').trigger("click");

                }
                else {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    })

                }
            }
        })

        return false;
    }



    showDeleteProductInModal = (url,title) => {

        $.ajax({

            type: "GET",
            url: url,
            success: function(res) {

                if (res.success === false) {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message
                    });

                }

                else {

                    $("#form-modal .modal-body").html(res);
                    $("#form-modal .modal-title").html(title);
                    $('#form-modal').modal('show');

                }

            }

        })
        
    }



    AjaxDeleteProduct = form => {

        $.ajax({

            type: "POST",
            url: form.action,
            contentType: false,
            processData: false,
            success: function(res) {

                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshProductTable();
                    $('#closemodal').trigger("click");

                }

                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    });
                }

            }


        })
        return false;
    }
   




    //------------------------------------------------------------------------------------------------------------------------------------------------



    refreshCategoryTable = () => {

        $.ajax({

            type: "GET",
            url: "Category/GetCategoriesViewComponent",
            success: function (res) {

                $('#categoryTable').html(res);

            }

        })

    }


    showAddCategoryInModal = (url, title) => {

        $.ajax({
            type: "GET",
            url: url,
            success: function(res) {

                $('#form-modal .modal-body').html(res);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal("show");

            }

        });

    }



    AjaxAddCategory = form => {

        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(res) {


                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshCategoryTable();
                    $('#closemodal').trigger('click');

                } else {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    });


                }

            }

            
    });
        return false;
    }



    ShowUpdateCategoryInModal = (url, title) => {

        $.ajax({
            type: "GET",
            url: url,
            success: function(res) {

                if (res.success === false) {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message
                    });

                } else {

                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');

                }

            }
        });
    }





    AjaxUpdateCategory = form => {

        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(res) {

                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshCategoryTable();
                    $('#closemodal').trigger('click');

                } else {


                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    });

                }

            }

        });
        return false;
    }




    ShowDeleteCategoryInModal = (url, title) => {

        $.ajax({
            type: "GET",
            url: url,
            success: function(res) {

                if (res.success === false) {

                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    });

                } else {

                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');

                }

            }

        });

    }


    AjaxDeleteCategory = form => {

        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(res) {

                if (res.success) {

                    Swal.fire({
                        icon: 'success',
                        title: res.message,
                        showConfirmButton: false,
                        timer: 1000
                    });

                    refreshCategoryTable();
                    $('#closemodal').trigger('click');

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: res.message,
                    });
                }

            }

        });

        return false;

    }


});

    


    

