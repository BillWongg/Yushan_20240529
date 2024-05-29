<script setup>
    import { ref } from 'vue'

    const backendResult = "";
    const message = ref("");
    const dataset = ref("");
    const SelectData = () => {
        let api = '/get/DB/Select?company=all'
        if (message.value !== "")
            api = '/get/DB/Select?company=' + message.value;
        fetch(api)
            .then(resp => resp.json())
            .then(json => {
                dataset.value = json;
                console.log(json);
            })
    }
</script>
<style>
    table {
        border-collapse: collapse;
        width: 100%;
        border: 1px inset #000000;
    }
    tr {
        border: 1px inset #000000;
    }
    th {
        border: 1px inset #000000;
    }
    td {
        border: 1px inset #000000;
    }
</style>
<template>
    <input style="width:300px; font-size:25px;" v-model="message" placeholder="請輸入公司名稱">
    <button style="font-size:20px;margin:10px;background-color:#0482fb;color:white;"  type="button" @click=SelectData>查詢</button>
   <table  >
       <thead>
           <tr  style="background-color:#f7e891;">
               <th>出表日期</th>
               <th>資料年月</th>
               <th>公司代號</th>
               <th>公司名稱</th>
               <th>產業別</th>
               <th>營業收入-當月營收</th>
               <th>營業收入-上月營收</th>
               <th>營業收入-去年當月營收</th>
               <th>營業收入-上月比較增減(%)</th>
               <th>營業收入-去年同月增減(%)</th>
               <th>累計營業收入-當月累計營收</th>
               <th>累計營業收入-去年累計營收</th>
               <th>累計營業收入-前期比較增減(%)</th>
               <th>備註</th>
           </tr>
       </thead>
       <tbody>
           <tr v-for="item in dataset" :key="item.publication_Date">
               <td>{{item.publication_Date}}</td>
               <td>{{item.publication_YM}}</td>
               <td>{{item.company_Code}}</td>
               <td>{{item.company_Name}}</td>
               <td>{{item.industry}}</td>
               <td>{{item.oI_TM}}</td>
               <td>{{item.oI_LM}}</td>
               <td>{{item.oI_TMLY}}</td>
               <td>{{item.oI_LM_ID}}</td>
               <td>{{item.oI_TMLY_ID}}</td>
               <td>{{item.diff_TM}}</td>
               <td>{{item.diff_LY}}</td>
               <td>{{item.diff_PC}}</td>
               <td>{{item.remark}}</td>
           </tr>
       </tbody>
   </table>
</template>
