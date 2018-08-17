$(function(){
	$(".chosen-select").chosen({no_results_text: "Aucune bdd de ce nom", width: "95%"});
	$("#removeDatabase-submit").click(function(event){
		event.preventDefault();
		var databaseName = $("#removeDatabase-databaseName").val();
		swal({
		  title: 'Êtes-vous sûr ?',
		  text: "La base de données "+databaseName+" sera effacée",
		  type: 'warning',
		  showCancelButton: true,
		  confirmButtonText: 'Oui',
		  cancelButtonText: 'Non',   
		  confirmButtonColor: "#DD6B55",
		}).then(function(isConfirm) {
		    if (isConfirm === true) {
		        $.ajax({
		            url: 'ajax/UserDatabases/removeDatabase', // La ressource ciblée
		            type: 'POST', // Le type de la requête HTTP.
		            data: { databaseName: databaseName },
		            dataType: 'json',
		            success: function (returnDat) {
		                swal(
		                    'Effacée',
		                    'La base de données à a bien été effacée.',
		                    'success'
		                ).then(function () {
		                    location.reload();
		                });
		            }
		        });
		    
		  } else if (isConfirm === false) {

		  } else {
		    // Esc, close button or outside click
		    // isConfirm is undefined
		  }
		});
	});
})