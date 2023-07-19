<template>

  <div class="headContainer">

    <div class="headContainerName">
      <h1>
        Superstore <br>
        Sales
      </h1>
    </div>

    <div class="headContainerStat">
      <h3 @click="openModal">
        Month: {{selectedDate}} <br>
        Orders per month: {{ stat1 }}<br>
        Total profit for the month: {{ stat2 }}<br>
        The amount of sales per month: {{ stat3 }}
      </h3>
      <div v-if="isModalOpen" class="modal">
        <div class="modal-content">
          <input type="text" v-model="selectedDate" placeholder="Example: 2012-09">
          <button @click="handleDateSelection">Save</button>
        </div>
      </div>
    </div>

  </div>


  <div>
    <div class="graphContainer">
      <div class="graphBox">
        <ChartOrderQuantity></ChartOrderQuantity>
      </div>

      <div class="graphBox">
        <div class="pieContainer">
          <div class="pieChart">
            <DonutChart ref="DC" />
          </div>
          
          <button @click="isDropdownOpen = !isDropdownOpen">{{PieGraphDate}}</button>
          <div v-if="isDropdownOpen" class="dropdown-menu">
            <div class="dropdown-item" v-for="option in optionsDropdown" :key="option" @click="selectOption(option)">
              {{ option }}
            </div>
          </div>

        </div>
      </div>

      <div class="graphBox">
        <ChartProfit></ChartProfit>
      </div>
      
      <div class="graphBox">
        <ChartSales></ChartSales>
      </div>
    </div>
  </div>

</template>




<script>
  import axios from 'axios';
  import ChartOrderQuantity from './components/ChartOrderQuantity.vue';
  import ChartProfit from './components/ChartProfit.vue';
  import ChartSales from './components/ChartSales.vue';
  import DonutChart from './components/PieVueChart.vue';

  export default {
    name: 'App',

    components: {
      ChartOrderQuantity,
      ChartProfit,
      ChartSales,
      DonutChart,
    },
    
    data() {
      return {
        localhost:'7162',

        data_OrderQuantity: null,
        data_Sales: null,
        data_Profit: null,
        data_UnitPrice: null,
        
        isModalOpen: false,
        selectedDate: '',
        stat1: '',
        stat2: '',
        stat3: '',

        PieGraphDate: 'All month',
        isDropdownOpen: false,
        optionsDropdown: null,

      }
    },
    
    mounted() { 
      axios.get('https://localhost:' + this.localhost + '/api/orders/LastMonth')
      .then(response => {
        this.selectedDate = response.data;
        this.getDataForSelectedDate(this.selectedDate);
      })
        .catch(error => {
          console.error(error);
      });
      
      axios.get('https://localhost:' + this.localhost + '/api/orders/DistinctYears')
      .then(response => {
        this.optionsDropdown = response.data;
      })
      .catch(error => {
        console.error(error);
      });
    },
    
    
    methods: {
      openModal() {
      this.isModalOpen = true;
      },

      handleDateSelection() {
        this.getDataForSelectedDate(this.selectedDate);
        this.isModalOpen = false;
      },

      getDataForSelectedDate(selectedDate) {   // запрос данных по дате
        const param1 = selectedDate;
        axios.get(`https://localhost:` + this.localhost + `/api/orders/MonthStat?month=${param1}`)
        .then(response => {
          this.stat1 = response.data.totalOrders;
          this.stat2 = response.data.totalProfit;
          this.stat3 = response.data.totalSales;
        })
          .catch(error => {
            console.error(error);
            this.stat1 = "no data available";
            this.stat2 = "no data available";
            this.stat3 = "no data available";
        });
      },

      selectOption(option) {
        this.PieGraphDate = option;
        this.getDataForPie(this.PieGraphDate);
        this.isDropdownOpen = false;
      },
      
      getDataForPie(PieGraphDate) {
        axios.get(`https://localhost:` + this.localhost + `/api/orders/CustomerSegmentCount/${PieGraphDate}`)
          .then(response => {
            this.$refs.DC.updateChart(response.data)
            //this.$emit('pieEvent');
            //console.log(response.data);
            //this.DonutChart.methods.updateChart();
          })
          .catch(error => {
            console.error(error);
          });
      }

    }

}


</script>



<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }

  /* HEAD */

  .headContainer {
    display: flex;
    align-items: center;
    height: 30vh;
  }
  .headContainerName {
    display: flex;
    align-items: center;
    text-align: left;
    padding: 10px;
  }
  .headContainerName h1 {
    font-size: 32px;
    margin: 50px;
  }
  .headContainerStat {
    margin-left: 5%;
    display: flex;
    align-items: center;
    text-align: left;
  }
  .headContainerName::after {
    content: "";
    margin-left: 0%;
    top: 5vh;
    width: 1px;
    height: 20vh;
    background-color: #ccc;
  }
  .headContainerStat h3 {
    transition: transform 0.3s, box-shadow 0.3s;
  }
  .headContainerStat h3:hover {
    transform: scale(1.05) translateY(-5px);
  }


  /* ВЫБОР ДАТЫ */
  .modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  }
  .modal-content {
    background-color: #fff;
    padding: 20px;
    border-radius: 20px;
    display: flex;
    justify-content: center;
  }
  .modal input {
    width: 100%;
    border-style: hidden;
    font-size: 24px;
  }
  .modal button {
    width: auto;
    height: 40px;
    margin-left: 10%;
    padding: 8px 16px;
    background-color: #5dafa4;
    color: #fff;
    border: none;
    cursor: pointer;
    border-radius: 10px;
    transition: transform 0.2s, box-shadow 0.2s;
  }
  .modal button:hover {
    transform: scale(1.05) translateY(-3px);
    box-shadow: inset 0 0 50px rgba(0, 0, 0, 0.5);
  }



  /* BODY */

  body {
    margin: 0;
  }

  .graphContainer {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    grid-template-rows: repeat(2, 1fr);
    gap: 3%; /* Расстояние между прямоугольниками */
    height: 100vh;
    background: rgb(25, 42, 35);
    background: linear-gradient(34deg, rgba(25, 42, 35, 1) 0%, rgba(41, 80, 65, 1) 24%, rgba(81, 128, 111, 1) 59%, rgba(64, 78, 76, 1) 100%);
    padding: 3%;
  }

  .graphBox {
    border-radius: 50px;
    padding: 20px;
    text-align: center;
    color: #fff;
    background: rgba(221, 236, 255, 0.0);
    box-shadow: inset 0 0 50px rgba(0, 0, 0, 0.3);
    transition: transform 0.3s, box-shadow 0.3s;
  }
  .graphBox:hover {
    transform: scale(1.05) translateY(-5px);
    box-shadow: inset 0 0 50px rgba(0, 0, 0, 0.5);
  }



  .pieContainer {
    align-items: center;
  }

  .pieChart {
    align-items: center;
  }

  .pieContainer button {
    margin-left: 75%;
    width: auto;
    height: 40px;
    padding: 8px 16px;
    background-color: #5dafa4;
    color: #fff;
    border: none;
    cursor: pointer;
    border-radius: 10px;
    transition: transform 0.2s, box-shadow 0.2s;
  }
  .pieContainer button:hover {
    transform: scale(1.05) translateY(-3px);
  }


  /* ВЫАПАДАЮЩИЙ СПИСОК */
  .dropdown-menu {
  position: absolute;
  top: 50%;
  left: 75%;
  
  border-radius: 4px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  padding: 0.5rem;
}

.dropdown-item {
  padding: 0.25rem 1.5rem;
  cursor: pointer;
  transition: background-color 0.3s;
}

.dropdown-item:hover {
  background-color: rgba(255, 255, 255, 0.2);
}

</style>
