class BulkProcess {
	constructor(bulkProcessType, elementId, selectorId, tableId) {
		this.bulkProcessType = bulkProcessType;
        this.elementId = elementId;
		this.selectorId = selectorId;
		this.tableId = tableId;
    }

	setup() {
		let self = this;
		$(self.elementId).on("click", function () {
			$(self.selectorId).toggle();
		});

		$('.dropzone').on("click",
			function () {
				$('#fileUpload').trigger("click");
			});

		$('#fileUpload').on("change", function (e) {
			$('.droparea').find(".dz-message").html("<div class='mt-3'>File uploaded: " + $(this).val().split('\\').pop() + "</div>");
		});

		// If you want to upload file on button click, then use below button click event
		$("#btnUpload").on('click', function () { 		// add jQuery ajax code to upload
			var files = $('#fileUpload').prop("files");
			var url = "/BulkProcess/StartProcess";

			var formData = new FormData();
			formData.append("BulkProcessType", self.bulkProcessType);
			formData.append("TableId", self.tableId);
			formData.append("UploadedFile", files[0]);

			jQuery.ajax({
				type: 'POST',
				url: url,
				data: formData,
				cache: false,
				contentType: false,
				processData: false,
				beforeSend: function (xhr) {
					xhr.setRequestHeader("XSRF-TOKEN",
						$('input:hidden[name="__RequestVerificationToken"]').val());
				},
				success: function (data) {
					if (data.result.success === true) {
						$('#' + self.bulkProcessType + 'UploadModal').modal('hide');

						abp.notify.info('This file is now processing, please see the status in the <a href="/BulkProcesses">bulk upload area</a>.');

						$('.data-tables-refresh .btn').trigger("click");
					}
					else {
						$('#uploadErrorMessage table tbody').empty();

						$('#uploadErrorMessage .message').html('<strong>Unfortunately error(s) have occured with this file, please correct them before processing can begin. These errors indicate the field does not match the correct data type, or a value is missing when it is required.</strong>');

						var errors = data.result.message;

						for (var i = 0; i < errors.length; i++) {
							var tableRow = "<tr>";

							var errorSplit = errors[i].split("\r\n");

							for (var o = 0; o < errorSplit.length; o++) {
								tableRow += "<td>" + errorSplit[o] + "</td>";
							}

							$('#uploadErrorMessage table tbody').append(tableRow += "</tr>");
						}

						$('#uploadErrorMessage').show();
					}
				}
			});
		});
    }
}