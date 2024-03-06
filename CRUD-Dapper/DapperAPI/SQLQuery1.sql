CREATE TABLE Students (      
  id int PRIMARY KEY IDENTITY,     
  admission_no VARCHAR(45) NOT NULL,  
  first_name VARCHAR(45) NOT NULL,      
  last_name VARCHAR(45) NOT NULL,  
  age int,  
  city VARCHAR(25) NOT NULL      
);

INSERT INTO Students (admission_no, first_name, last_name, age, city) VALUES
(3354,'Luisa', 'Evans', 13, 'Texas'),       
(2135, 'Paul', 'Ward', 15, 'Alaska'),       
(4321, 'Peter', 'Bennett', 14, 'California'),    
(4213,'Carlos', 'Patterson', 17, 'New York'),       
(5112, 'Rose', 'Huges', 16, 'Florida'),  
(6113, 'Marielia', 'Simmons', 15, 'Arizona'),    
(7555,'Antonio', 'Butler', 14, 'New York'),       
(8345, 'Diego', 'Cox', 13, 'California');