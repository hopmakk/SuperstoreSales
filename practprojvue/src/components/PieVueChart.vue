  <template>
    <Doughnut :data="data" :options="options" style="pie"/>
  </template>
  
  <script>
  import axios from 'axios';
  import 'chart.js/auto';
  import { Doughnut } from 'vue-chartjs'

  export default {
    name: 'PieVueChart',
    components: {
      Doughnut,
    },
    computed: {
      chartData() { return this.data }
    },
    data() {
      return {
        localhost:'7162',

        data: {
          labels: [],
          datasets: [{data: [0,0,0,0,0,0,0]}]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            plugins: {
                    title: {
                        display: true,
                        text: 'Count of orders for customer segments',
                        color: 'white',
                        font: {
                        size: 18,
                        },
                    },


                    legend: {
                        labels: {
                        color: 'white',
                        },
                    },
                },
        },
        type: 'doughnut',
      }
    },


    mounted() {
        axios.get('https://localhost:' + this.localhost + '/api/orders/CustomerSegmentCount')
        .then(response => {
            this.updateChart(response.data)
        })
        .catch(error => {
            console.error(error);
        });
    },


    methods: {
        updateChart(newData) {
            this.data = {
                labels: [],
                datasets: [{}]
            },
            this.data.labels = newData.map(item => item.customerSegment)
            this.data.datasets[0].data = newData.map(item => item.count)
        },
    }
  }
  </script>