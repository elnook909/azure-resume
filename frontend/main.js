window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const functionApiUrl = 'https://getresumecountermb.azurewebsites.net/api/VisitorCounterFunction?';
const localFunctionApi = 'http://localhost:7071/api/VisitorCounterFunction';

const getVisitCount = () => {
    let count = 30;
    fetch(functionApiUrl).then(response => {
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
