window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const localFunctionApi = 'http://localhost:7071/api/VisitorCounterFunction';

const getVisitCount = () => {
    let count = 30;
    fetch(localFunctionApi).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website called function API.");
        count =  response.count;
        document.getElementById("counter").textContent = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}
