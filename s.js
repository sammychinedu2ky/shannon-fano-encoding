function returnWordProb(word) {
    let obj = {}
    let arr = word.split('');
    for (i of arr) {
        let word = i;
        let count = 0;
        arr.forEach(element => {
            if (element == word) count++
        });
        obj[i] = count
    }
    let sorted = {}
   let arrOfObj= Object.keys(obj).sort((a,b)=>obj[b]>obj[a])
 
   arrOfObj.forEach(i=>{
       sorted[i]=(obj[i]/word.length).toFixed(2)
   })
   return sorted
}

function comparisonArr(start,wordValues){
   let newArr = wordValues.slice(start);
   let indexArr = [];
   indexArr.length=newArr.length;
   indexArr.fill(1);
   
   let diff = []
   for (let i=0; i<newArr.length-1; i++){
     let left = newArr.slice(0,i+1)
     let right = newArr.slice(i+1)
  
     let leftSum = left.reduce((a,b)=>a+b)
     let rightSum = right.reduce((a,b)=>a+b)
     diff.push(Math.abs(leftSum-rightSum))
   }
   let min = Math.min(...diff)
   let indexOfMin = diff.indexOf(min);

   indexArr.fill(0,0,indexOfMin+1)
   console.log(indexArr)
   return [indexArr,indexArr.indexOf(1)];

  
}

function reduceStages(stages){
    let newStages = []
    for (let i=0; i<stages[0].length; i++){
        let stage=''
        for (let j=0; j<stages.length; j++){
            stage+=stages[j][i]
        }
        newStages.push(stage)
    }
    return newStages;
}

function reduceZerosToOne(reducedStages){
    return reducedStages.map(i=>{
        let splitIndices = i.split('')
        if(splitIndices[splitIndices.length-1]==0){
            let numOfZeros = 0;
            splitIndices.forEach(j=>{
                if(j=='0')numOfZeros++
            })
            let numberOfZerosToDelete = numOfZeros-1
            for(let i=0; i<numberOfZerosToDelete; i++) splitIndices.pop()
        }
        return splitIndices.join("")
    })
}
function shanon(word){
    let wordAndProbabilities = returnWordProb(word)
    let wordValues = Object.values(wordAndProbabilities);
    wordValues = wordValues.map(i=>parseFloat(i))
    let lengthOfCharacters = wordValues.length
    let stages = []
    let shouldContinue = true;
    let start = 0;
    while(shouldContinue){
       let stage =  comparisonArr(start,wordValues)
       stages.push(stage[0]);
       start+=stage[1]
       if(stage[0].length==2)shouldContinue=false
        
    }
  stages.forEach(i=>{
    let lengthOfChild = i.length
    let diffInLength = lengthOfCharacters-lengthOfChild
    if(diffInLength!==0){
      for(let j=0; j<diffInLength; j++){
       
        i.unshift(0)
      }
    }
  })

let reducedStages = reduceStages(stages)
let finalStage = reduceZerosToOne(reducedStages)
let answer = {}
let keys = Object.keys(wordAndProbabilities)
keys.forEach((i,index)=>{
    answer[i]=finalStage[index]
})
return answer
}
let word = 'ABRACADABRA'
word="SAMSON"
console.log(shanon(word))