## ECO DEFENDERS - ENERGY CLASH

![ECO DEFENDERS - ENERGY CLASH](./images/intro.png)

![ECO DEFENDERS - ENERGY CLASH](./images/zombie_.png)


To run the frontend of questionnaire:
Clone the branch feature /frondend and open it in VSCode. Then, type following commands in the terminal.
cd quiz-client
npm start

To run the backend of questionnaire :
Clone the branch feature/backend and open it in IntelliJ IDEA
Run QuizappApplication

Since the build files of the Unity opens the game in browser, make sure to include the browser URL of the game inside the brackets of allowed origins in the following file in backend. src/test/java/com/example/demo/QuizappAopplicationTests.java


-- Connect to the questiondb database
\c questiondb;


-- Insert data into the question table
INSERT INTO question (question_id, option1, option2, option3, option4, question_title, feedback1, feedback2, feedback3, feedback4, general_feedback, right_answer_index)
VALUES

    -- Question 1
    (1, 'Solar power', 'Wind power', 'Fossil fuels', 'Hydropower', 'What is the primary source of energy for most power grids around the world?', 
    'Solar power is growing but is not the primary source globally.', 
    'Wind power is significant in some areas but not the main source worldwide.', 
    'Correct! Fossil fuels, including coal, natural gas, and oil, are currently the primary energy source for most power grids.', 
    'Hydropower is a key renewable source but not the primary source globally.', 
    'While the mix of energy sources varies by region, fossil fuels remain the dominant source for electricity generation globally, though renewable sources are on the rise.', 2),

    -- Question 2
    (2, 'Through water pipes', 'Via transmission and distribution networks', 'Directly from generators to homes', 'Through the internet', 
    'How does electricity typically travel from power plants to consumers?', 
    'Water pipes are used for plumbing, not electrical transmission.', 
    'Correct! Transmission and distribution networks are essential for delivering electricity from power plants to consumers.', 
    'Electricity must be transmitted and distributed over networks; it doesn\'t go directly from generators to homes.', 
    'The internet is a network for data, not electricity.', 
    'Electricity is generated at power plants and then transmitted over long distances via high-voltage transmission lines. It\'s then distributed to consumers through lower-voltage distribution networks.', 1),

    -- Question 3
    (3, 'It increases energy consumption', 'It leads to higher energy costs', 'It reduces energy bills and environmental impact', 'It has no impact on the environment', 
    'Why is energy efficiency important in homes and businesses?', 
    'Energy efficiency aims to reduce, not increase, consumption.', 
    'The goal of energy efficiency is to lower costs, not raise them.', 
    'Correct! Energy efficiency helps in saving on energy bills and reducing the environmental impact.', 
    'Energy efficiency has a significant positive impact on the environment by reducing emissions.', 
    'Energy efficiency is crucial for reducing energy consumption, lowering energy bills, and minimizing the environmental footprint by decreasing greenhouse gas emissions.', 2),

    -- Question 4
    (4, 'To increase the overall energy consumption', 'To balance energy supply and demand', 'To eliminate the use of renewable energy sources', 'To double the energy costs for consumers', 
    'What is the primary goal of demand management in energy usage?', 
    'This is the opposite of demand management\'s goal, which aims to optimize, not increase, energy use.', 
    'Correct! Balancing energy supply and demand helps improve grid reliability and efficiency.', 
    'Demand management often encourages the integration of renewable energy sources, not their elimination.', 
    'The goal is to potentially lower or optimize costs, not increase them.', 
    'Demand management aims to ensure energy is used more efficiently, balancing the supply with the fluctuating demand to maintain grid stability and reduce costs.', 1),

    -- Question 5
    (5, 'Increasing energy prices during off-peak hours', 'Providing incentives for high energy consumption', 'Offering lower rates or incentives for using less energy during peak times', 'Discouraging the use of energy-efficient appliances', 
    'What is a common method used in demand management to encourage lower energy use during peak hours?', 
    'This approach would not encourage lower usage during peak times.', 
    'Incentives are typically offered for reducing consumption, not increasing it.', 
    'Correct! Incentives for lower usage during peak hours help manage demand effectively.', 
    'Energy-efficient appliances are actually encouraged as part of demand management strategies.', 
    'Lowering rates or providing incentives for reduced energy use during peak hours helps smooth out energy demand, benefiting both the grid and the consumer.', 2),

    -- Question 6
    (6, 'Higher energy bills', 'Less control over their energy use', 'Savings on their electricity bill', 'Reduced internet connectivity', 
    'Benefits to the consumer from demand management include:', 
    'Demand management aims to reduce, not increase, consumer energy bills.', 
    'It actually offers more control over energy use and costs.', 
    'Correct! One of the key benefits for consumers is the potential for savings on their electricity bills.', 
    'Demand management focuses on energy consumption, not affecting internet connectivity.', 
    'Participating in demand management programs can lead to significant savings on electricity bills for consumers by incentivizing energy use during off-peak hours.', 2),

    -- Question 7
    (7, 'By significantly increasing carbon emissions', 'By reducing reliance on fossil fuels and lowering carbon emissions', 'By eliminating the need for public transportation', 'By discouraging the use of renewable energy', 
    'How does implementing demand management strategies benefit the environment?', 
    'Demand management aims to decrease, not increase, carbon emissions.', 
    'Correct! Reducing reliance on fossil fuels and lowering carbon emissions are key environmental benefits of demand management.', 
    'Demand management strategies do not involve transportation policies directly.', 
    'These strategies typically encourage, rather than discourage, the use of renewable energy sources.', 
    'Implementing demand management strategies plays a crucial role in environmental conservation by reducing the reliance on non-renewable energy sources and minimizing carbon emissions.', 1),

    -- Question 8
    (8, 'Higher energy bills', 'Less control over their energy use', 'Savings on their electricity bill', 'Reduced internet connectivity', 
    'What can be a direct benefit of participating in a demand management program for consumers?', 
    'The goal of demand management is to offer savings, not to increase bills.', 
    'Participants typically gain greater control and flexibility over their energy use.', 
    'Correct! Saving on electricity bills is a significant benefit for consumers who participate in demand management programs.', 
    'Demand management does not impact internet connectivity.', 
    'Participation in demand management programs often results in direct benefits for consumers, such as savings on electricity bills, by encouraging energy use during less expensive, off-peak hours.', 2),

    -- Question 9
    (9, 'It increases the energy load during peak times', 'It shifts energy usage to times when demand is higher', 'It helps balance the power grid by using energy during lower-demand periods', 'It makes energy more expensive during off-peak hours', 
    'Why is load shifting important in demand management?', 
    'The purpose of load shifting is to decrease, not increase, the load during peak times to help balance energy demand.', 
    'Shifting energy usage to higher demand times would counteract the goals of demand management, which seeks to alleviate these peaks.', 
    'Correct! By shifting energy use to lower-demand periods, we can achieve a more balanced and efficient use of the power grid.', 
    'Load shifting is designed to take advantage of lower costs during off-peak hours, not to make energy more expensive.', 
    'Load shifting is a critical component of demand management, aimed at moving energy use from peak to off-peak hours. This helps balance the power grid, reduces the need for additional power plants, and can lead to cost savings for consumers and utility providers alike.', 2),

    -- Question 10
    (10, 'Fixed lighting systems in public areas', 'Emergency medical equipment', 'Residential air conditioning units', 'Data centers that require constant cooling', 
    'Which of the following electric loads can be effectively managed as part of a demand management program?', 
    'While lighting can be managed, fixed systems in public areas often have safety implications that limit their flexibility.', 
    'Emergency medical equipment is critical and cannot be subject to demand management due to the risk to human life.', 
    'Correct! Residential air conditioning units are a significant and flexible load that can be adjusted to enhance grid efficiency without compromising comfort significantly.', 
    'Data centers have strict cooling requirements for operational integrity and may not offer the flexibility required for effective demand management.', 
    'Demand management programs focus on adjusting the usage of flexible and non-critical electric loads to optimize energy consumption. Residential air conditioning units, for example, can be adjusted without compromising safety or critical operations, making them ideal for inclusion in these programs.', 2);
