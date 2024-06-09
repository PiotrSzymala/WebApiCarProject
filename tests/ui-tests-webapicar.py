
import random
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from webdriver_manager.chrome import ChromeDriverManager

chrome_options = Options()
chrome_options.add_argument('--no-sandbox')
chrome_options.add_experimental_option("detach", True)
chrome_options.add_argument('--disable-dev-shm-usage')
chrome_options.add_argument('--disable-blink-features=AutomationControlled')  
service = Service(ChromeDriverManager().install())  

def generate_random_username(base_name="testuser"):
    random_number = random.randint(1000, 9999)
    return f"{base_name}{random_number}"

def generate_random_car_data():
    random_number = random.randint(1, 999)
    return f"{random_number}"

driver = webdriver.Chrome(service=service, options=chrome_options)

try:
    driver.get("https://webapicarproject.onrender.com")

    register_button = driver.find_element(By.XPATH, "//button[text()='Register']")
    register_button.click()

    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.ID, "login")))

    login = driver.find_element(By.ID, "login")
    password = driver.find_element(By.ID, "passwd")

    random_username = generate_random_username()


    login.send_keys(random_username)
    password.send_keys("password123")


    submit_button = driver.find_element(By.XPATH, "//button[@type='submit' and text()='Register']")
    submit_button.click()

    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.XPATH, "//button[@type='submit' and text()='Login']")))
    login = driver.find_element(By.ID, "login")
    password = driver.find_element(By.ID, "passwd")

    login.send_keys(random_username)
    password.send_keys("password123")

    login_button = driver.find_element(By.XPATH, "//button[@type='submit' and text()='Login']")
    login_button.click()


    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.XPATH, "//button[@class='add-car-button' and text()='Add Car']")))

    brand = "Volkswagen" + generate_random_car_data()
    model = "Passat" + generate_random_car_data()
    year = "2024"
    registry_plate = "ABC123" + generate_random_car_data()
    vin_number = "ABC123" + generate_random_car_data()
    available = True  


    add_car_button = driver.find_element(By.XPATH, "//button[@class='add-car-button' and text()='Add Car']")
    
    add_car_button.click()
    

    brand_input = driver.find_element(By.XPATH, "//input[@formcontrolname='brand']")
    model_input = driver.find_element(By.XPATH, "//input[@formcontrolname='model']")
    year_input = driver.find_element(By.XPATH, "//input[@formcontrolname='year']")
    registry_plate_input = driver.find_element(By.XPATH, "//input[@formcontrolname='registryPlate']")
    vin_number_input = driver.find_element(By.XPATH, "//input[@formcontrolname='vinNumber']")
    available_input = driver.find_element(By.XPATH, "//input[@formcontrolname='isAvailable']")

    brand_input.send_keys(brand)
    model_input.send_keys(model)
    year_input.send_keys(year)
    registry_plate_input.send_keys(registry_plate)
    vin_number_input.send_keys(vin_number)
    if available:
        available_input.click()

    submit_button = driver.find_element(By.XPATH, "//button[@type='submit' and text()='Add']")
    
    submit_button.click()

    logout_button = driver.find_element(By.XPATH, "//button[text()='Logout']")


finally:
    
    driver.quit()
