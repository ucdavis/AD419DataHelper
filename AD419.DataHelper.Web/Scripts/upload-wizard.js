function UploadWizardFactory() {

    function reset() {
        $('#uploadButton').prop('disabled', false);
        $('#uploadForm')[0].reset();

        $('#reviewStepper').removeClass('active');
        $('#confirmStepper').removeClass('active');

        $('#uploadTab').addClass('active');
        $('#progressTab').removeClass('active');
        $('#errorTab').removeClass('active');
        $('#reviewTab').removeClass('active');
        $('#confirmTab').removeClass('active');
    }
        
    function setup(options) {

        $('#uploadForm')
            .submit(function (event) {
                $('#uploadButton').prop('disabled', true);
                $('#uploadTab').removeClass('active');
                $('#loadingTab').addClass('active');

                event.preventDefault();

                var formData = new FormData($(this)[0]);

                // fetch table
                $.post({
                    url: options.uploadUrl,
                    data: formData,
                    cache: false,
                    processData: false,
                    contentType: false
                })
                .success(function (view) {
                    $('#review').html(view);
                    drawTable(options.tableOptions);
                })
                .error(function(err) {
                    console.log(err);
                    $('#loadingTab').removeClass('active');
                    $('#errorTab').addClass('active');
                });
            });

        $('#resetErrorButton')
            .click(function() {
                reset();
            });

        $('#returnUploadButton')
            .click(function () {
                $('#uploadButton').prop('disabled', false);
                $('#uploadTab').addClass('active');
                $('#reviewTab').removeClass('active');
                $('#reviewStepper').removeClass('active');
            });

        $('#returnReviewButton')
            .click(function () {
                $('#reviewTab').addClass('active');
                $('#confirmTab').removeClass('active');
                $('#confirmStepper').removeClass('active');
            });

        $('#continueConfirmButton')
            .click(function () {
                $('#reviewTab').removeClass('active');
                $('#confirmTab').addClass('active');
                $('#confirmStepper').addClass('active');
            });

        $('#confirmButton')
            .click(function () {
                $('#confirmForm').submit();
            });
    }

    function drawTable(options) {
        var config = {
            lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
            initComplete: function() {
                $('#loadingTab').removeClass('active');
                $('#reviewTab').addClass('active');
                $('#reviewStepper').addClass('active');
            }
        };

        if (options) {
            $.extend(config, options);
        }

        return $('#uploadTable').DataTable(config);
    }

    return {
        reset: reset,
        setup: setup
    }
}

var UploadWizard = UploadWizardFactory();
