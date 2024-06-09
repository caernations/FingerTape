import csv
import random
from data.faker import Faker

fake = Faker('id_ID')

# Enum lists
jenis_kelamin_list = ['Laki-Laki', 'Perempuan']
status_perkawinan_list = ['belum menikah', 'menikah', 'cerai']
golongan_darah_list = ['A', 'B', 'AB', 'O']
agama_list = ['Islam', 'Kristen', 'Katolik', 'Hindu', 'Buddha', 'Konghucu']
pekerjaan_list = ['PNS', 'Karyawan Swasta', 'Wiraswasta', 'Pelajar', 'Mahasiswa', 'Tidak Bekerja']
kewarganegaraan_list = ['WNI', 'WNA']

# Function to generate a random NIK
def generate_nik():
    return ''.join([str(random.randint(0, 9)) for _ in range(16)])

# Create a list of dictionaries with the data
data = []
for _ in range(500):
    profile = {
        'NIK': generate_nik(),
        'nama': fake.name(),
        'tempat_lahir': fake.city(),
        'tanggal_lahir': fake.date_of_birth().strftime('%Y-%m-%d'),
        'jenis_kelamin': random.choice(jenis_kelamin_list),
        'golongan_darah': random.choice(golongan_darah_list),
        'alamat': fake.address(),
        'agama': random.choice(agama_list),
        'status_perkawinan': random.choice(status_perkawinan_list),
        'pekerjaan': random.choice(pekerjaan_list),
        'kewarganegaraan': random.choice(kewarganegaraan_list),
    }
    data.append(profile)

# Define CSV file path
csv_file_path = '/mnt/data/data_nik.csv'

# Write data to CSV file
with open(csv_file_path, mode='w', newline='', encoding='utf-8') as file:
    writer = csv.DictWriter(file, fieldnames=data[0].keys())
    writer.writeheader()
    writer.writerows(data)

csv_file_path
