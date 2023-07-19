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

        data_Profit: null,
      }
    },

    mounted() {
      axios.get('https://localhost:' + this.localhost + '/api/orders/Profit')
      .then(response => {
        this.data_Profit = response.data;
        this.renderChart();
      })
        .catch(error => {
          console.error(error);
      });
    },

    methods: {
      renderChart() {
        const ctx = this.$refs.chart.getContext('2d');

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: this.data_Profit.map(item => item.date), // "date" в качестве меток на X
                datasets: [
                    {
                        label: "",
                        data: this.data_Profit.map(item => item.values), // "values" в качестве данных
                        borderColor: 'white',
                        backgroundColor : 'red',
                    }
                ]
            },
            options: {
                scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                    color: 'white',
                    font: {
                        size: 14,
                    },
                    },
                },
                x: {
                    beginAtZero: true,
                    ticks: {
                    color: 'white',
                    font: {
                        size: 14,
                    },
                    },
                },
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Average of Profit',
                        color: 'white', //цвет заголовка
                        font: {
                        size: 18,
                        },
                    },

                    legend: {
                        display: false,
                    },
                },
            },
        });

      },
    },
  };
  </script>
  