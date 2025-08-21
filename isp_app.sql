DROP DATABASE IF EXISTS isp_app;
CREATE DATABASE IF NOT EXISTS isp_app CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE isp_app;

CREATE TABLE IF NOT EXISTS `plans` (
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  speed VARCHAR(50) NOT NULL,
  monthly_payment DECIMAL(10, 2) NOT NULL,
  data_limit DECIMAL(10, 2) NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `users` (
  id VARCHAR(50) PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  email VARCHAR(100) UNIQUE NOT NULL,
  phone VARCHAR(20),
  password VARCHAR(255) NOT NULL,
  plan_id INT NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (plan_id) REFERENCES plans(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `data_usages` (
  id INT AUTO_INCREMENT PRIMARY KEY,
  user_id VARCHAR(50) NOT NULL,
  start_date DATE NOT NULL,
  end_date DATE NOT NULL,
  used DECIMAL(10, 2) DEFAULT 0,
  `limit` DECIMAL(10, 2) NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `daily_usages` (
  id INT AUTO_INCREMENT PRIMARY KEY,
  data_usage_id INT NOT NULL,
  date DATE NOT NULL,
  download DECIMAL(10, 2) DEFAULT 0,
  upload DECIMAL(10, 2) DEFAULT 0,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (data_usage_id) REFERENCES data_usages(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO plans (name, speed, monthly_payment, data_limit) VALUES
('Internet 100 Mbps', '100 Mbps', 29.99, 500.00),
('Internet 300 Mbps', '300 Mbps', 49.99, 1000.00),
('Internet 1 Gbps', '1 Gbps', 79.99, 2000.00);

INSERT INTO users (id, name, email, phone, password, plan_id) VALUES
('user_guest', 'Guest User', 'guest@isp.com', '+1234567890', '$2a$10$uhlivU7MYq0vxdjFAJLigOBsEUKGW7XlqT6kq/cbAIeXzB3ZEmjw2', 1),
('user_123', 'Premium User', 'premium@isp.com', '+0987654321', '$2a$10$uhlivU7MYq0vxdjFAJLigOBsEUKGW7XlqT6kq/cbAIeXzB3ZEmjw2', 3);

INSERT INTO data_usages (user_id, start_date, end_date, used, `limit`) VALUES
('user_guest', CURDATE() - INTERVAL 30 DAY, CURDATE() + INTERVAL 5 DAY, 150.00, 500.00),
('user_123', CURDATE() - INTERVAL 30 DAY, CURDATE() + INTERVAL 5 DAY, 750.00, 2000.00);

INSERT INTO daily_usages (data_usage_id, date, download, upload) VALUES
-- user_guest (data_usage_id = 1)
(1, CURDATE() - INTERVAL 1 DAY, 5.2, 1.1),
(1, CURDATE(), 4.8, 0.9),

-- user_123 (data_usage_id = 2)  
(2, CURDATE() - INTERVAL 1 DAY, 25.7, 5.2),
(2, CURDATE(), 23.4, 4.7);

SELECT * FROM plans;
SELECT * FROM users;
SELECT * FROM data_usages;
SELECT * FROM daily_usages WHERE date = CURDATE();
