  <template>
    <div>
      <canvas ref="chart"></canvas>
    </div>
  </template>
  
  <script>
  import Chart from 'chart.js/auto';
  import axios from 'axios';

  export default {
    
    data() {
      return {
        localhost:'7162',

        data_DoughnutmerSegment: null,
        ctxConst: null,
        chart1: null,
      }
    },

    mounted() {
        this.ctxConst = this.$refs.chart.getContext('2d');

        axios.get('https://localhost:' + this.localhost + '/api/orders/CustomerSegmentCount')
        .then(response => {
            this.data_DoughnutmerSegment = response.data;
            this.renderChart();
        })
        .catch(error => {
            console.error(error);
        });
        
    },

    methods: {
      renderChart() {
        //const ctx = this.$refs.chart.getContext('2d');

        this.chart1 = new Chart(this.ctxConst, {
            type: "doughnut",
            data: {
                labels: this.data_DoughnutmerSegment.map(item => item.customerSegment),
                datasets: [
                    {
                        data: this.data_DoughnutmerSegment.map(item => item.count),
                    }
                ]
            },
            options: {
                
                maintainAspectRatio: false,
                
                plugins: {
                    title: {
                        display: true,
                        text: 'Count of orders for Doughnutmer segments',
                        color: 'white', //цвет заголовка
                        font: {
                        size: 18,
                        },
                    },


                    legend: {
                        labels: {
                        color: 'white', // Устанавливаем цвет текста легенды на белый
                        },
                    },
                },
            },
        });
      },



      updateData(data) {
        this.data_DoughnutmerSegment = data;
        console.log(this.data_DoughnutmerSegment);
      }

    },
  };
  </script>
  